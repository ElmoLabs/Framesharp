function Datagrid(options, $target) {

    var self = this;

    var $datagrid = $target;
    var $datagridForm = $datagrid.closest('form');
    var $filterForm = $datagridForm.siblings('form')[0] ? $($datagridForm.siblings('form')[0]) : $datagridForm;
    var $pagination = $datagrid.siblings('[data-pagination]');
    var activeFormDefaultValues = {};

    options = options || {};

    self.target = $datagrid;

    self.onChange = options.onChange || function () { return true; };
    self.onCreate = options.onCreate || function () { return true; };
    self.onDelete = options.onDelete || function () { return true; };
    self.onReload = options.onReload || function () { return true; };
    self.onSave   = options.onSave   || function () { return true; };
    self.onUpdate = options.onUpdate || function () { return true; };

    self.reload = function () {

        return reload();
    };

    function addRecord($addButton) {

        // fecha todos os formul�rios de cadastro/edi��o que est�o
        // abertos antes de exibir o formul�rio de cadastro
        closeActiveForms();

        // linha de adi��o de novo registro
        var $addRecordRow = $datagrid.find('[data-edit-row]:first');

        // armazena os valores default dos campos do formul�rio em uma vari�vel de backup para que
        // possam ser restaurados uma vez que o cadastro do registro seja finalizado ou cancelado
        collectFormDefaultValues($addRecordRow);

        // Desabilita o bot�o de ativa��o da linha que cadastra um novo registro
        $addButton.attr('disabled', 'disabled');

        hideNoRecordsMessage();

        // Exibe a linha de cadastro de novo registro
        $addRecordRow.show();
    }

    function cancelCreate($cancelButton) {

        var $editModeRow = $cancelButton.closest('[data-edit-row]');
        var $addButton = $datagrid.find('[data-new]:first');

        // limpa os erros de valida��o do formul�rio, para que na pr�xima vez que o formul�rio
        // seja aberto, os campos n�o venham validados.
        clearFormValidation();

        // restaura os valores originais que o formul�rio possuia antes de come�ar a ser editado
        restoreFormInputsDefaultValues($editModeRow);

        $editModeRow.hide();

        // verifica se o datagrid n�o possui nenhum item e caso 
        // positivo exibe a mensagem de nenhum item encontrado
        if ($datagrid.find('[data-display-row]').length == 0)
            showNoRecordsMessage();

        $addButton.removeAttr('disabled');
    }

    function cancelUpdate($cancelButton) {

        var $editModeRow = $cancelButton.closest('[data-edit-row]');
        var $displayModeRow = $editModeRow.prev('[data-display-row]');

        // limpa os erros de valida��o do formul�rio, para que na pr�xima vez que o formul�rio
        // seja aberto, os campos n�o venham validados.
        clearFormValidation();

        // restaura os valores originais que o formul�rio possuia antes de come�ar a ser editado
        restoreFormInputsDefaultValues($editModeRow);

        $editModeRow.hide();
        $displayModeRow.show();
    }

    function clearFormValidation() {

        var validator = $datagridForm.validate();

        validator.resetForm();

        // Limpa os erros de valida��o dos inputs do formul�rio
        $datagridForm.find(".input-validation-error").removeClass("input-validation-error").addClass('valid');

        // Limpa as mensagens de valida��o dos inputs do formul�rio
        $datagridForm.find(".field-validation-error").removeClass("field-validation-error").addClass('field-validation-valid').empty();
    }

    function closeActiveForms() {

        // busca todos os formul�rios de cadastro/edi��o que est�o abertos e dispara um 
        // clique no bot�o de cancelar de cada um deles para que o formul�rio seja fechado
        $datagrid.find('[data-edit-row]:visible').each(function (index, editModeRow) {

            var $editModeRow = $(editModeRow);

            var $cancelButton = $editModeRow.find('[data-cancel]');

            $cancelButton.click();
        });
    }

    function collectFormDefaultValues($editModeRow) {

        activeFormDefaultValues = {};

        $editModeRow.find('input, select').each(function (index, field) {

            var $field = $(field);
            var fieldName = $field.attr('name');
            var originalValue = null;

            if ($field.is(':checkbox'))
                originalValue = $field.is(':checked') ? 'True' : 'False';
            else
                originalValue = $field.val();

            activeFormDefaultValues[fieldName] = originalValue;
        });

        return activeFormDefaultValues;
    }

    function createRecord($saveButton) {

        // linha do datagrid com os campos de cadastro de um novo registro
        var $editModeRow = $saveButton.closest('[data-edit-row]');

        // bot�o de adi��o de novo registro
        var $addButton = $datagrid.find('[data-new]:first');

        // valida as informa��es preenchidas no formul�rio, e exibe a lista de erros se existir algum
        $datagridForm.validate();

        // verifica se o formul�rio � v�lido, e caso contr�rio, cancela a request ao servidor
        if (!$datagridForm.valid())
            return false;

        // desabilita o bot�o de salvar para evitar que o request seja duplicado 
        // caso um segundo clique seja efetuado no bot�o
        $saveButton.attr('disabled', 'disabled');

        // url da action respons�vel por salvar um novo registro
        var url = $saveButton.attr('href');

        // objeto com todos os dados do formul�rio de cria��o do registro que 
        // ser� enviado no corpo do request
        var requestData = {};

        collectFormData($editModeRow, requestData);

        $.post(url, requestData, function (responseData, responseResult, responseMetaData) {

            // Checa o status do resultado da opera��o enviada nos headers do response
            var operationResultStatus = responseMetaData.getResponseHeader('OperationResultStatus');

            // Obt�m o html retornado pelo servidor
            var $responseHtml = $(responseData);

            // Verifica se houve erro no cadastro do novo item
            if (operationResultStatus == "Failed") {

                // atualiza o formul�rio de cadastro com o formul�rio validado retornado no response
                $editModeRow.replaceWith($responseHtml);

                // ativa os controles de escuta de eventos, contidos no html trazido pelo response
                initializeEventListeners($responseHtml);
                initializeEventTriggers($responseHtml);

                // Torna o formul�rio vis�vel no datagrid
                $responseHtml.show();

                // Exibe a mensagem de erro com o erro que impediu que o registro fosse salvo.
                // Essa mensagem vem no header do response enviado pelo servidor.
                notifier.showErrorMessage(responseMetaData.getResponseHeader('OperationResultMessage'));

                return false;
            }

            // Adiciona uma nova linha no datagrid com o novo registro criado logo ap�s a primeira linha
            // referente ao formul�rio de cria��o de registro
            $editModeRow.after($responseHtml);

            // ativa os controles de escuta de eventos, contidos no html trazido pelo response
            initializeEventListeners($responseHtml);
            initializeEventTriggers($responseHtml);

            // caso exista algum datagrid dentro do response retornado, seus eventos s�o inicializados
            window.datagrid($responseHtml.find('[data-datagrid]'));

            // Reabilita o bot�o de adi��o de registro
            $addButton.removeAttr('disabled');

            // reabilita o bot�o de salvar registro do formul�rio de cria��o
            $saveButton.removeAttr('disabled');

            // uma vez que o registro foi criado com sucesso, os campos do formul�rio
            // de cria��o s�o restaurados para seus valores originais, para que na pr�xima
            // vez que o formul�rio de cria��o seja aberto, n�o venha preenchido com dados
            // do registro que foi cadastrado anteriormente.
            restoreFormInputsDefaultValues($editModeRow);

            // esconde o formul�rio de cria��o de registro
            $editModeRow.hide();

            // exibe a mensagem de sucesso retornada pelo servidor atrav�s do header do response
            notifier.showSuccessMessage(responseMetaData.getResponseHeader('OperationResultMessage'));

            // executa a fun��o de callback definida para altera��es no grid
            self.onChange();

            // executa a fun��o de callback definida para quando um registro � salvo
            self.onSave();

            // executa a fun��o de callback definida para quando um novo registro � criado
            self.onCreate();

            return true;

        }).fail(function (responseMetaData, responseStatus, errorThrown) {

            console.log(responseStatus + ' - ' + errorThrown);
            console.log(responseMetaData);
        });

        return true;
    }

    function deleteRecord($deleteButton) {

        if (!confirm('Deseja realmente excluir este item?'))
            return false;

        // a linha a qual pertence o bot�o de deletar � a linha de exibi��o
        var $displayModeRow = $deleteButton.closest('[data-display-row]');
        // a pr�xima linha abaixo da linha de exibi��o � a linha de edi��o do registro
        var $editModeRow = $displayModeRow.next('[data-edit-row]');
        // a pr�xima linha abaixo da linha de edi��o � a linha de detalhamento
        var $detailsRow = $editModeRow.next('[data-details-row]');

        // desabilita o bot�o de excluir para evitar que o request seja duplicado
        $deleteButton.attr('disabled', 'disabled');

        // conjunto de informa��es do formul�rio a ser salvo
        var requestData = {};

        // url que ser� usada para invocar a action que exclui o registro
        var url = $deleteButton.attr('href');

        collectFormData($editModeRow, requestData);

        $.post(url, requestData, function (responseData, responseStatus, responseMetaData) {

            // Checa o status do resultado da opera��o enviada nos headers do response
            var operationResultStatus = responseMetaData.getResponseHeader('OperationResultStatus');

            // Caso o status enviado no response seja de falha, significa que o registro n�o pode ser deletado.
            if (operationResultStatus == "Failed") {

                // Exibe a mensagem de erro com o erro que impediu que o registro fosse deletado.
                // Essa mensagem vem no header do response enviado pelo servidor.
                notifier.showErrorMessage(responseMetaData.getResponseHeader('OperationResultMessage'));

                // reabilita o bot�o de exclus�o uma vez que n�o foi poss�vel remover o registro
                $deleteButton.removeAttr('disabled');

                return false;
            }

            // remove todas as linhas do grid relativas ao registro deletado
            $editModeRow.remove();
            $displayModeRow.remove();
            $detailsRow.remove();

            // verifica se o item que foi removido era o �ltimo item listado e 
            // caso positivo exibe a mensagem de nenhum item encontrado
            if ($datagrid.find('[data-display-row]').length == 0)
                showNoRecordsMessage();

            // exibe a mensagem de sucesso retornada pelo servidor atrav�s do header do response
            notifier.showSuccessMessage(responseMetaData.getResponseHeader('OperationResultMessage'));

            // executa a fun��o de callback definida para altera��es no grid
            self.onChange();

            // executa a fun��o de callback definida para quando um registro � removido
            self.onDelete();

            return true;

        }).fail(function (responseMetaData, responseStatus, errorThrown) {

            console.log(responseStatus + ' - ' + errorThrown);
            console.log(responseMetaData);
        });

        return true;
    }

    function editRecord($editButton) {

        // fecha todos os formul�rios de cadastro/edi��o que est�o
        // abertos antes de exibir o formul�rio de cadastro
        closeActiveForms();

        // linha de exibi��o das informa��es do registro que ser� editado
        var $displayModeRow = $editButton.closest('[data-display-row]');

        // linha que cont�m o formul�rio de edi��o do registro
        var $editModeRow = $displayModeRow.next('[data-edit-row]');

        // armazena os valores default dos campos do formul�rio em uma vari�vel de backup para que
        // possam ser restaurados uma vez que o cadastro do registro seja finalizado ou cancelado
        collectFormDefaultValues($editModeRow);

        // esconde a linha de exibi��o do registro que est� sendo editado
        $displayModeRow.hide();

        // exibe a linha que cont�m o formul�rio de edi��o do registro
        $editModeRow.show();
    }
    
    function executeAction($actionButton) {
        
        // linha na qual o bot�o de a��o est� contido
        var $displayModeRow = $actionButton.closest('[data-display-row]');
        // linha do formul�rio do registro, onde est�o contidas as suas informa��es chave
        var $editModeRow = $displayModeRow.next('[data-edit-row]');
        
        // desabilita o bot�o de a��o para evitar que o request seja duplicado
        $actionButton.attr('disabled', 'disabled');

        // conjunto de informa��es do formul�rio a ser salvo
        var requestData = {};

        // url que ser� usada para invocar a a��o do bot�o
        var url = $actionButton.attr('data-action');

        // coleta os dados que ser�o enviados no request
        collectFormData($editModeRow, requestData);

        $.post(url, requestData, function (responseData, responseStatus, responseMetaData) {

            // Checa o status do resultado da opera��o enviada nos headers do response
            var operationResultStatus = responseMetaData.getResponseHeader('OperationResultStatus');

            // Caso o status enviado no response seja de falha, significa que o registro n�o pode ser deletado.
            if (operationResultStatus == "Failed") {

                // Exibe a mensagem de erro que ocorreu no servidor e foi devolvida atrav�s do header do response.
                notifier.showErrorMessage(responseMetaData.getResponseHeader('OperationResultMessage'));

                // reabilita o bot�o de a��o
                $actionButton.removeAttr('disabled');

                return false;
            }

            // atualiza o datagrid
            self.reload();
            
            // exibe a mensagem de sucesso devolvida pelo servidor atrav�s do header do response.
            notifier.showSuccessMessage(responseMetaData.getResponseHeader('OperationResultMessage'));
            
            return true;

        }).fail(function (responseMetaData, responseStatus, errorThrown) {

            console.log(responseStatus + ' - ' + errorThrown);
            console.log(responseMetaData);
        });

        return true;
    }

    function paginate($selectedPage) {

        // checa se a p�gina selecionada � valida para a pagina��o. Se a p�gina estiver desabilitada
        // ou for a p�gina que est� sendo visualizada no momento, cancela o evento de pagina��o
        var pageIsNotAccessible = $selectedPage.parent().hasClass('disabled') || $selectedPage.parent().hasClass('active');

        // caso a p�gina clicada n�o esteja dispon�vel, a execu��o do evento � encerrada
        if (pageIsNotAccessible)
            return false;

        var $selectedPageInput = $('[data-current-page]');

        // substitui o valor do input hidden relativo � p�gina corrente, com o n�mero da p�gina selecionada.
        $selectedPageInput.val($selectedPage.attr('data-page-number'));

        // verifica se o input hidden da p�gina selecionada est� contido no formul�rio de filtro
        // caso n�o esteja, o input � copiado para dentro do formul�rio de filtro antes que esse
        // seja submetido ao servidor
        if (!$selectedPageInput.closest('form').is($filterForm))
            $filterForm.append($selectedPageInput);

        // submete o formul�rio de filtro do datagrid com o campo de n�mero da p�gina 
        // atualizado para o valor selecionado, atrav�s do evento de reload do datagrid
        return self.reload();
    }

    function reload() {

        var url = $filterForm.attr('action');

        var formData = {};

        $filterForm.find('input:not(:checkbox), select').each(function (index, item) {
            formData[$(item).attr('name')] = $(item).val();
        });

        $filterForm.find('input[type="checkbox"]').each(function (index, item) {
            formData[$(item).attr('name')] = $(item).is(':checked') ? 'True' : 'False';
        });

        $datagrid.block({ message: null });

        $.post(url, formData, function (responseData, responseResult, responseMetaData) {

            var $reloadedDatagridContainer = $(responseData);

            $filterForm.before($reloadedDatagridContainer);

            $datagrid.remove();
            $pagination.remove();
            $filterForm.remove();
            $datagridForm.remove();

            window.datagrid($reloadedDatagridContainer.find('[data-datagrid]'));

            // ativa os controles de escuta de eventos, contidos no html trazido pelo response
            initializeEventListeners($reloadedDatagridContainer);
            initializeEventTriggers($reloadedDatagridContainer);

            $datagrid.unblock();
            
            var operationResultStatus = responseMetaData.getResponseHeader('OperationResultStatus');
            
            if (operationResultStatus != null) {

                var message = responseMetaData.getResponseHeader('OperationResultMessage');

                if (operationResultStatus == "Failed")
                    notifier.showErrorMessage(message);
                if (operationResultStatus == "Succeeded")
                    notifier.showErrorMessage(message);
            }

            return self.onReload();
        });
    };

    function restoreFormInputsDefaultValues($editModeRow) {

        $editModeRow.find('input, select').each(function (index, field) {

            var $field = $(field);
            var fieldName = $field.attr('name');
            var originalValue = activeFormDefaultValues[fieldName];

            if ($field.is(':checkbox'))
                return $field.prop('checked', (originalValue == 'True' ? true : false)).change();

            return $field.val(originalValue).change();
        });

        activeFormDefaultValues = {};
    }

    function showNoRecordsMessage() {

        // se o datagrid estiver dentro de outro datagrid, a mensagem n�o � exibida
        if ($datagrid.closest('[data-datagrid]').length > 0)
            return false;

        var tdCount = 0;

        $datagrid.find('thead tr:first th').each(function (index, th) {
            $(th).attr('colspan') ? tdCount += $(th).attr('colspan') : tdCount++;
        });

        var $messageRow = $("<tr data-no-records-message><td style='text-align: center' colspan='" + tdCount + "'><i>Nenhum registro encontrado</i></td></tr>");

        $datagrid.find('> tbody').prepend($messageRow);
    }

    function hideNoRecordsMessage() {

        $datagrid.find('[data-no-records-message]').remove();
    }

    function sort($sortColumn) {

        // input hidden que armazena o valor do crit�rio de ordena��o do grid
        var $sortCriteriaFormInput = $filterForm.find('[data-sort-criteria]');

        // input hidden que armazena o tipo de ordena��o do grid
        var $sortOrderFormInput = $filterForm.find('[data-sort-order]');

        // obt�m o crit�rio de ordena��o a partir da coluna clicada
        var selectedSortCriteria = $sortColumn.data('sort-column');

        // obt�m o crit�rio de ordena��o corrente
        var currentSortCriteria = $sortCriteriaFormInput.val();

        // se o crit�rio de ordena��o selecionado for diferente do crit�rio de 
        // ordena��o corrente significa que outra coluna foi clicada na ordena��o
        if (selectedSortCriteria != currentSortCriteria) {
            // define, no input do formul�rio, o novo crit�rio de ordena��o que ser� usado
            $sortCriteriaFormInput.val(selectedSortCriteria);

            // define, no input do formul�rio, o tipo de ordena��o para ascendente
            $sortOrderFormInput.val('Ascending');
        }
            // caso o crit�rio de ordena��o selecionado seja igual ao crit�rio 
            // de ordena��o corrente, significa que o a mesma coluna de ordena��o
            // foi clicada, nesse caso apenas o tipo de ordena��o deve ser definido
        else {
            // busca o tipo de ordena��o corrente
            var currentSortOrder = $sortOrderFormInput.val();

            // se o tipo de ordena��o corrente for ascendente, � trocado para descendente
            if (currentSortOrder == 'Ascending') {
                $sortOrderFormInput.val('Descending');
            }
                // caso contr�rio � trocado para ascendente
            else {
                $sortOrderFormInput.val('Ascending');
            }
        }

        // uma vez que os inputs hidden relativos a ordena��o tiveram seu valor atualizado
        // de acordo com a coluna de ordena��o clicada, realiza o submit do formul�rio de
        // filtro do datagrid atrav�s do evento de reload
        return self.reload();
    }

    function updateRecord($saveButton) {

        // linha do datagrid com os campos de edi��o do registro existente
        var $editModeRow = $saveButton.closest('[data-edit-row]');

        // valida as informa��es preenchidas no formul�rio, e exibe a lista de erros se existir algum
        $datagridForm.validate();

        // verifica se o formul�rio � v�lido, e caso contr�rio, cancela a request ao servidor
        if (!$datagridForm.valid())
            return false;

        // desabilita o bot�o de salvar para evitar que o request seja duplicado 
        // caso um segundo clique seja efetuado no bot�o
        $saveButton.attr('disabled', 'disabled');

        // url da action respons�vel por atualizar um registro existente
        var url = $saveButton.attr('href');

        // objeto com todos os dados do formul�rio de cria��o do registro que 
        // ser� enviado no corpo do request
        var requestData = {};

        collectFormData($editModeRow, requestData);

        $.post(url, requestData, function (responseData, responseResult, responseMetaData) {

            // Checa o status do resultado da opera��o enviada nos headers do response
            var operationResultStatus = responseMetaData.getResponseHeader('OperationResultStatus');

            // Obt�m o html retornado pelo servidor
            var $responseHtml = $(responseData);

            // Verifica se houve erro no cadastro do novo item
            if (operationResultStatus == "Failed") {

                // atualiza o formul�rio de cadastro com o formul�rio validado retornado no response
                $editModeRow.replaceWith($responseHtml);

                // ativa os controles de escuta de eventos, contidos no html trazido pelo response
                initializeEventListeners($responseHtml);
                initializeEventTriggers($responseHtml);

                // Torna o formul�rio vis�vel no datagrid
                $responseHtml.show();

                // Exibe a mensagem de erro com o erro que impediu que o registro fosse salvo.
                // Essa mensagem vem no header do response enviado pelo servidor.
                notifier.showErrorMessage(responseMetaData.getResponseHeader('OperationResultMessage'));

                return false;
            }

            // linha de exibi��o do registro que est� sendo editado no datagrid
            var $displayModeRow = $editModeRow.prev('[data-display-row]');

            // linha de detalhes do registro que est� sendo editado no datagrid
            var $detailsRow = $editModeRow.next('[data-details-row]');

            // insere no datagrid uma linha com as informa��es do registro atualizado
            $displayModeRow.before($responseHtml);

            // remove do datagrid as linhas com os dados antigos do registro atualizado
            $editModeRow.remove();
            $displayModeRow.remove();
            $detailsRow.remove();

            // ativa os controles de escuta de eventos, contidos no html trazido pelo response
            initializeEventListeners($responseHtml);
            initializeEventTriggers($responseHtml);

            // caso exista algum datagrid dentro do response retornado, seus eventos s�o inicializados
            window.datagrid($responseHtml.find('[data-datagrid]'));

            // exibe a mensagem de sucesso retornada pelo servidor atrav�s do header do response
            notifier.showSuccessMessage(responseMetaData.getResponseHeader('OperationResultMessage'));

            // executa a fun��o de callback definida para altera��es no grid
            self.onChange();

            // executa a fun��o de callback definida para quando um registro � salvo
            self.onSave();

            // executa a fun��o de callback definida para quando um registro � atualizado
            self.onUpdate();

            return true;

        }).fail(function (responseMetaData, responseStatus, errorThrown) {

            console.log(responseStatus + ' - ' + errorThrown);
            console.log(responseMetaData);
        });
    }

    function initializeEventHandlers() {

        $datagrid.on('click', '[data-action]', function (event) {

            event.preventDefault();
            event.stopPropagation();

            // bot�o que abre o modo de edi��o do registro
            var $actionButton = $(event.currentTarget);

            executeAction($actionButton);
        });

        $datagrid.on('click', '[data-cancel]', function (event) {

            event.preventDefault();
            event.stopPropagation();

            var $addButton = $datagrid.find('[data-new]:first');
            var $cancelButton = $(event.currentTarget);

            if ($addButton.is(':disabled'))
                cancelCreate($cancelButton);
            else
                cancelUpdate($cancelButton);
        });

        $datagrid.on('click', '[data-delete]', function (event) {

            event.preventDefault();
            event.stopPropagation();

            // bot�o de exclus�o do registro
            var $deleteButton = $(event.currentTarget);

            deleteRecord($deleteButton);
        });

        $datagrid.on('click', '[data-edit]', function (event) {

            event.preventDefault();
            event.stopPropagation();

            // bot�o que abre o modo de edi��o do registro
            var $editButton = $(event.currentTarget);

            editRecord($editButton);
        });

        $datagrid.on('click', '[data-new]', function (event) {

            event.preventDefault();
            event.stopPropagation();

            var $addButton = $(event.currentTarget);

            addRecord($addButton);
        });

        $pagination.on('click', '[data-page-number]', function (event) {

            event.preventDefault();
            event.stopPropagation();

            var $selectedPage = $(event.currentTarget);

            return paginate($selectedPage);
        });

        $datagrid.on('click', '[data-reload]', function (event) {

            event.preventDefault();
            event.stopPropagation();

            self.reload();
        });

        $datagrid.on('click', '[data-save]', function (event) {

            event.preventDefault();
            event.stopPropagation();

            var $addButton = $datagrid.find('[data-new]:first');
            var $saveButton = $(event.currentTarget);

            if ($addButton.is(':disabled'))
                createRecord($saveButton);
            else
                updateRecord($saveButton);
        });

        $datagrid.on('click', '[data-sort-column]', function (event) {

            event.preventDefault();
            event.stopPropagation();

            // coluna de ordena��o que recebeu o clique
            var $sortColumn = $(event.currentTarget);

            return sort($sortColumn);
        });

        $filterForm.on('submit', function (event) {

            event.preventDefault();
            event.stopPropagation();

            self.reload();
        });
    }

    function initializeDatagrid() {

        // verifica se o datagrid n�o possui nenhum item e caso 
        // positivo exibe a mensagem de nenhum item encontrado
        if ($datagrid.find('[data-display-row]').length == 0)
            showNoRecordsMessage();

        initializeEventHandlers();
    }

    initializeDatagrid();

    return self;
}

/*
 * Fun��o global respons�vel por inicializar um novo datagrid a partir de um elemento html na p�gina.
 *
 * selector: seletor css que indica o elemento html que corresponde ao novo datagrid
 * options: objeto contendo os par�metros adicionais para a inicializa��o do datagrid, 
 * os valores aceitos s�o: onChange, onCreate, onDelete, onReload, onSave, onUpdate.
 */
window.datagrid = function (selector, options) {

    // cria uma lista tempor�ria onde ser�o armazenados os objetos datagrid inicializados
    // para que ao final da execu��o do m�todo esses objetos possam ser retornardos
    var initializedDatagridList = [];

    // inicializa um datagrid para cada elemento que for encontrado a partir do seletor informado
    $(selector).each(function (index, item) {

        // inicializa o datagrid e armazena o objeto retornado
        var datagridObject = new Datagrid(options, $(item));

        // adiciona o objeto criado na lista global de datagrids da p�gina
        window.datagridList.push(datagridObject);

        // adiciona o objeto criado na lista tempor�ria de datagrids inicializados a partir do seletor informado
        initializedDatagridList.push(datagridObject);
    });

    // caso nenhum datagrid tenha sido inicializado a partir do seletor informado retorna nulo
    if (initializedDatagridList.length == 0)
        return null;

    // caso o seletor informado aponte para mais de um elemento, � retornada uma lista contendo 
    // todos os objetos de datagrid criados, caso contr�rio, � retornado apenas um �nico objeto
    return initializedDatagridList.length > 1 ? initializedDatagridList : initializedDatagridList[0];
};

/*
 * Vari�vel global onde ficam armazenadas todas as inst�ncias de datagrid existentes na p�gina
 */
window.datagridList = [];

/*
* Extens�o customizada para os objetos jQuery que obt�m o objeto datagrid ao qual um determinado elemento informado
* da p�gina esteja associado. Caso n�o exista nenhum objeto datagrid associado ao elemento, � retornado nulo
*/
$.fn.getDatagrid = function () {

    // objeto jQuery a partir do qual a chamada foi executada
    var $target = $(this);
    
    // inicializa a vari�vel onde ser� armazenado o objeto datagrid associado ao elemento html da p�gina
    var matchingItem = null;

    // percorre a lista global que armazena todos os objetos datagrid presentes na p�gina, verificando
    // se algum deles corresponde ao elemento html informado na chamada da fun��o
    for (var index in window.datagridList) {

        // se o objeto corrente da lista corresponder ao objeto datagrid que est� sendo procurado, o
        // loop � interrompido e o objeto encontrado � armazenado na vari�vel criada no passo anterior
        if (window.datagridList[index].target.is($target)) {
            matchingItem = window.datagridList[index];
            break;
        }
    }

    // retorna o valor da vari�vel criada. Caso tenha sido encontrado um objeto de datagrid correspondente
    // ao elemento informado, este ser� retornado, caso contr�rio, ser� retornado null.
    return matchingItem;

};

// inicializa todos os datagrids existentes na p�gina no momento que ela for carregada
$(document).ready(function () {

    window.datagrid('[data-datagrid]');
});