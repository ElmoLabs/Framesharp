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

        // fecha todos os formulários de cadastro/edição que estão
        // abertos antes de exibir o formulário de cadastro
        closeActiveForms();

        // linha de adição de novo registro
        var $addRecordRow = $datagrid.find('[data-edit-row]:first');

        // armazena os valores default dos campos do formulário em uma variável de backup para que
        // possam ser restaurados uma vez que o cadastro do registro seja finalizado ou cancelado
        collectFormDefaultValues($addRecordRow);

        // Desabilita o botão de ativação da linha que cadastra um novo registro
        $addButton.attr('disabled', 'disabled');

        hideNoRecordsMessage();

        // Exibe a linha de cadastro de novo registro
        $addRecordRow.show();
    }

    function cancelCreate($cancelButton) {

        var $editModeRow = $cancelButton.closest('[data-edit-row]');
        var $addButton = $datagrid.find('[data-new]:first');

        // limpa os erros de validação do formulário, para que na próxima vez que o formulário
        // seja aberto, os campos não venham validados.
        clearFormValidation();

        // restaura os valores originais que o formulário possuia antes de começar a ser editado
        restoreFormInputsDefaultValues($editModeRow);

        $editModeRow.hide();

        // verifica se o datagrid não possui nenhum item e caso 
        // positivo exibe a mensagem de nenhum item encontrado
        if ($datagrid.find('[data-display-row]').length == 0)
            showNoRecordsMessage();

        $addButton.removeAttr('disabled');
    }

    function cancelUpdate($cancelButton) {

        var $editModeRow = $cancelButton.closest('[data-edit-row]');
        var $displayModeRow = $editModeRow.prev('[data-display-row]');

        // limpa os erros de validação do formulário, para que na próxima vez que o formulário
        // seja aberto, os campos não venham validados.
        clearFormValidation();

        // restaura os valores originais que o formulário possuia antes de começar a ser editado
        restoreFormInputsDefaultValues($editModeRow);

        $editModeRow.hide();
        $displayModeRow.show();
    }

    function clearFormValidation() {

        var validator = $datagridForm.validate();

        validator.resetForm();

        // Limpa os erros de validação dos inputs do formulário
        $datagridForm.find(".input-validation-error").removeClass("input-validation-error").addClass('valid');

        // Limpa as mensagens de validação dos inputs do formulário
        $datagridForm.find(".field-validation-error").removeClass("field-validation-error").addClass('field-validation-valid').empty();
    }

    function closeActiveForms() {

        // busca todos os formulários de cadastro/edição que estão abertos e dispara um 
        // clique no botão de cancelar de cada um deles para que o formulário seja fechado
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

        // botão de adição de novo registro
        var $addButton = $datagrid.find('[data-new]:first');

        // valida as informações preenchidas no formulário, e exibe a lista de erros se existir algum
        $datagridForm.validate();

        // verifica se o formulário é válido, e caso contrário, cancela a request ao servidor
        if (!$datagridForm.valid())
            return false;

        // desabilita o botão de salvar para evitar que o request seja duplicado 
        // caso um segundo clique seja efetuado no botão
        $saveButton.attr('disabled', 'disabled');

        // url da action responsável por salvar um novo registro
        var url = $saveButton.attr('href');

        // objeto com todos os dados do formulário de criação do registro que 
        // será enviado no corpo do request
        var requestData = {};

        collectFormData($editModeRow, requestData);

        $.post(url, requestData, function (responseData, responseResult, responseMetaData) {

            // Checa o status do resultado da operação enviada nos headers do response
            var operationResultStatus = responseMetaData.getResponseHeader('OperationResultStatus');

            // Obtém o html retornado pelo servidor
            var $responseHtml = $(responseData);

            // Verifica se houve erro no cadastro do novo item
            if (operationResultStatus == "Failed") {

                // atualiza o formulário de cadastro com o formulário validado retornado no response
                $editModeRow.replaceWith($responseHtml);

                // ativa os controles de escuta de eventos, contidos no html trazido pelo response
                initializeEventListeners($responseHtml);
                initializeEventTriggers($responseHtml);

                // Torna o formulário visível no datagrid
                $responseHtml.show();

                // Exibe a mensagem de erro com o erro que impediu que o registro fosse salvo.
                // Essa mensagem vem no header do response enviado pelo servidor.
                notifier.showErrorMessage(responseMetaData.getResponseHeader('OperationResultMessage'));

                return false;
            }

            // Adiciona uma nova linha no datagrid com o novo registro criado logo após a primeira linha
            // referente ao formulário de criação de registro
            $editModeRow.after($responseHtml);

            // ativa os controles de escuta de eventos, contidos no html trazido pelo response
            initializeEventListeners($responseHtml);
            initializeEventTriggers($responseHtml);

            // caso exista algum datagrid dentro do response retornado, seus eventos são inicializados
            window.datagrid($responseHtml.find('[data-datagrid]'));

            // Reabilita o botão de adição de registro
            $addButton.removeAttr('disabled');

            // reabilita o botão de salvar registro do formulário de criação
            $saveButton.removeAttr('disabled');

            // uma vez que o registro foi criado com sucesso, os campos do formulário
            // de criação são restaurados para seus valores originais, para que na próxima
            // vez que o formulário de criação seja aberto, não venha preenchido com dados
            // do registro que foi cadastrado anteriormente.
            restoreFormInputsDefaultValues($editModeRow);

            // esconde o formulário de criação de registro
            $editModeRow.hide();

            // exibe a mensagem de sucesso retornada pelo servidor através do header do response
            notifier.showSuccessMessage(responseMetaData.getResponseHeader('OperationResultMessage'));

            // executa a função de callback definida para alterações no grid
            self.onChange();

            // executa a função de callback definida para quando um registro é salvo
            self.onSave();

            // executa a função de callback definida para quando um novo registro é criado
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

        // a linha a qual pertence o botão de deletar é a linha de exibição
        var $displayModeRow = $deleteButton.closest('[data-display-row]');
        // a próxima linha abaixo da linha de exibição é a linha de edição do registro
        var $editModeRow = $displayModeRow.next('[data-edit-row]');
        // a próxima linha abaixo da linha de edição é a linha de detalhamento
        var $detailsRow = $editModeRow.next('[data-details-row]');

        // desabilita o botão de excluir para evitar que o request seja duplicado
        $deleteButton.attr('disabled', 'disabled');

        // conjunto de informações do formulário a ser salvo
        var requestData = {};

        // url que será usada para invocar a action que exclui o registro
        var url = $deleteButton.attr('href');

        collectFormData($editModeRow, requestData);

        $.post(url, requestData, function (responseData, responseStatus, responseMetaData) {

            // Checa o status do resultado da operação enviada nos headers do response
            var operationResultStatus = responseMetaData.getResponseHeader('OperationResultStatus');

            // Caso o status enviado no response seja de falha, significa que o registro não pode ser deletado.
            if (operationResultStatus == "Failed") {

                // Exibe a mensagem de erro com o erro que impediu que o registro fosse deletado.
                // Essa mensagem vem no header do response enviado pelo servidor.
                notifier.showErrorMessage(responseMetaData.getResponseHeader('OperationResultMessage'));

                // reabilita o botão de exclusão uma vez que não foi possível remover o registro
                $deleteButton.removeAttr('disabled');

                return false;
            }

            // remove todas as linhas do grid relativas ao registro deletado
            $editModeRow.remove();
            $displayModeRow.remove();
            $detailsRow.remove();

            // verifica se o item que foi removido era o último item listado e 
            // caso positivo exibe a mensagem de nenhum item encontrado
            if ($datagrid.find('[data-display-row]').length == 0)
                showNoRecordsMessage();

            // exibe a mensagem de sucesso retornada pelo servidor através do header do response
            notifier.showSuccessMessage(responseMetaData.getResponseHeader('OperationResultMessage'));

            // executa a função de callback definida para alterações no grid
            self.onChange();

            // executa a função de callback definida para quando um registro é removido
            self.onDelete();

            return true;

        }).fail(function (responseMetaData, responseStatus, errorThrown) {

            console.log(responseStatus + ' - ' + errorThrown);
            console.log(responseMetaData);
        });

        return true;
    }

    function editRecord($editButton) {

        // fecha todos os formulários de cadastro/edição que estão
        // abertos antes de exibir o formulário de cadastro
        closeActiveForms();

        // linha de exibição das informações do registro que será editado
        var $displayModeRow = $editButton.closest('[data-display-row]');

        // linha que contém o formulário de edição do registro
        var $editModeRow = $displayModeRow.next('[data-edit-row]');

        // armazena os valores default dos campos do formulário em uma variável de backup para que
        // possam ser restaurados uma vez que o cadastro do registro seja finalizado ou cancelado
        collectFormDefaultValues($editModeRow);

        // esconde a linha de exibição do registro que está sendo editado
        $displayModeRow.hide();

        // exibe a linha que contém o formulário de edição do registro
        $editModeRow.show();
    }
    
    function executeAction($actionButton) {
        
        // linha na qual o botão de ação está contido
        var $displayModeRow = $actionButton.closest('[data-display-row]');
        // linha do formulário do registro, onde estão contidas as suas informações chave
        var $editModeRow = $displayModeRow.next('[data-edit-row]');
        
        // desabilita o botão de ação para evitar que o request seja duplicado
        $actionButton.attr('disabled', 'disabled');

        // conjunto de informações do formulário a ser salvo
        var requestData = {};

        // url que será usada para invocar a ação do botão
        var url = $actionButton.attr('data-action');

        // coleta os dados que serão enviados no request
        collectFormData($editModeRow, requestData);

        $.post(url, requestData, function (responseData, responseStatus, responseMetaData) {

            // Checa o status do resultado da operação enviada nos headers do response
            var operationResultStatus = responseMetaData.getResponseHeader('OperationResultStatus');

            // Caso o status enviado no response seja de falha, significa que o registro não pode ser deletado.
            if (operationResultStatus == "Failed") {

                // Exibe a mensagem de erro que ocorreu no servidor e foi devolvida através do header do response.
                notifier.showErrorMessage(responseMetaData.getResponseHeader('OperationResultMessage'));

                // reabilita o botão de ação
                $actionButton.removeAttr('disabled');

                return false;
            }

            // atualiza o datagrid
            self.reload();
            
            // exibe a mensagem de sucesso devolvida pelo servidor através do header do response.
            notifier.showSuccessMessage(responseMetaData.getResponseHeader('OperationResultMessage'));
            
            return true;

        }).fail(function (responseMetaData, responseStatus, errorThrown) {

            console.log(responseStatus + ' - ' + errorThrown);
            console.log(responseMetaData);
        });

        return true;
    }

    function paginate($selectedPage) {

        // checa se a página selecionada é valida para a paginação. Se a página estiver desabilitada
        // ou for a página que está sendo visualizada no momento, cancela o evento de paginação
        var pageIsNotAccessible = $selectedPage.parent().hasClass('disabled') || $selectedPage.parent().hasClass('active');

        // caso a página clicada não esteja disponível, a execução do evento é encerrada
        if (pageIsNotAccessible)
            return false;

        var $selectedPageInput = $('[data-current-page]');

        // substitui o valor do input hidden relativo à página corrente, com o número da página selecionada.
        $selectedPageInput.val($selectedPage.attr('data-page-number'));

        // verifica se o input hidden da página selecionada está contido no formulário de filtro
        // caso não esteja, o input é copiado para dentro do formulário de filtro antes que esse
        // seja submetido ao servidor
        if (!$selectedPageInput.closest('form').is($filterForm))
            $filterForm.append($selectedPageInput);

        // submete o formulário de filtro do datagrid com o campo de número da página 
        // atualizado para o valor selecionado, através do evento de reload do datagrid
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

        // se o datagrid estiver dentro de outro datagrid, a mensagem não é exibida
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

        // input hidden que armazena o valor do critério de ordenação do grid
        var $sortCriteriaFormInput = $filterForm.find('[data-sort-criteria]');

        // input hidden que armazena o tipo de ordenação do grid
        var $sortOrderFormInput = $filterForm.find('[data-sort-order]');

        // obtém o critério de ordenação a partir da coluna clicada
        var selectedSortCriteria = $sortColumn.data('sort-column');

        // obtém o critério de ordenação corrente
        var currentSortCriteria = $sortCriteriaFormInput.val();

        // se o critério de ordenação selecionado for diferente do critério de 
        // ordenação corrente significa que outra coluna foi clicada na ordenação
        if (selectedSortCriteria != currentSortCriteria) {
            // define, no input do formulário, o novo critério de ordenação que será usado
            $sortCriteriaFormInput.val(selectedSortCriteria);

            // define, no input do formulário, o tipo de ordenação para ascendente
            $sortOrderFormInput.val('Ascending');
        }
            // caso o critério de ordenação selecionado seja igual ao critério 
            // de ordenação corrente, significa que o a mesma coluna de ordenação
            // foi clicada, nesse caso apenas o tipo de ordenação deve ser definido
        else {
            // busca o tipo de ordenação corrente
            var currentSortOrder = $sortOrderFormInput.val();

            // se o tipo de ordenação corrente for ascendente, é trocado para descendente
            if (currentSortOrder == 'Ascending') {
                $sortOrderFormInput.val('Descending');
            }
                // caso contrário é trocado para ascendente
            else {
                $sortOrderFormInput.val('Ascending');
            }
        }

        // uma vez que os inputs hidden relativos a ordenação tiveram seu valor atualizado
        // de acordo com a coluna de ordenação clicada, realiza o submit do formulário de
        // filtro do datagrid através do evento de reload
        return self.reload();
    }

    function updateRecord($saveButton) {

        // linha do datagrid com os campos de edição do registro existente
        var $editModeRow = $saveButton.closest('[data-edit-row]');

        // valida as informações preenchidas no formulário, e exibe a lista de erros se existir algum
        $datagridForm.validate();

        // verifica se o formulário é válido, e caso contrário, cancela a request ao servidor
        if (!$datagridForm.valid())
            return false;

        // desabilita o botão de salvar para evitar que o request seja duplicado 
        // caso um segundo clique seja efetuado no botão
        $saveButton.attr('disabled', 'disabled');

        // url da action responsável por atualizar um registro existente
        var url = $saveButton.attr('href');

        // objeto com todos os dados do formulário de criação do registro que 
        // será enviado no corpo do request
        var requestData = {};

        collectFormData($editModeRow, requestData);

        $.post(url, requestData, function (responseData, responseResult, responseMetaData) {

            // Checa o status do resultado da operação enviada nos headers do response
            var operationResultStatus = responseMetaData.getResponseHeader('OperationResultStatus');

            // Obtém o html retornado pelo servidor
            var $responseHtml = $(responseData);

            // Verifica se houve erro no cadastro do novo item
            if (operationResultStatus == "Failed") {

                // atualiza o formulário de cadastro com o formulário validado retornado no response
                $editModeRow.replaceWith($responseHtml);

                // ativa os controles de escuta de eventos, contidos no html trazido pelo response
                initializeEventListeners($responseHtml);
                initializeEventTriggers($responseHtml);

                // Torna o formulário visível no datagrid
                $responseHtml.show();

                // Exibe a mensagem de erro com o erro que impediu que o registro fosse salvo.
                // Essa mensagem vem no header do response enviado pelo servidor.
                notifier.showErrorMessage(responseMetaData.getResponseHeader('OperationResultMessage'));

                return false;
            }

            // linha de exibição do registro que está sendo editado no datagrid
            var $displayModeRow = $editModeRow.prev('[data-display-row]');

            // linha de detalhes do registro que está sendo editado no datagrid
            var $detailsRow = $editModeRow.next('[data-details-row]');

            // insere no datagrid uma linha com as informações do registro atualizado
            $displayModeRow.before($responseHtml);

            // remove do datagrid as linhas com os dados antigos do registro atualizado
            $editModeRow.remove();
            $displayModeRow.remove();
            $detailsRow.remove();

            // ativa os controles de escuta de eventos, contidos no html trazido pelo response
            initializeEventListeners($responseHtml);
            initializeEventTriggers($responseHtml);

            // caso exista algum datagrid dentro do response retornado, seus eventos são inicializados
            window.datagrid($responseHtml.find('[data-datagrid]'));

            // exibe a mensagem de sucesso retornada pelo servidor através do header do response
            notifier.showSuccessMessage(responseMetaData.getResponseHeader('OperationResultMessage'));

            // executa a função de callback definida para alterações no grid
            self.onChange();

            // executa a função de callback definida para quando um registro é salvo
            self.onSave();

            // executa a função de callback definida para quando um registro é atualizado
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

            // botão que abre o modo de edição do registro
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

            // botão de exclusão do registro
            var $deleteButton = $(event.currentTarget);

            deleteRecord($deleteButton);
        });

        $datagrid.on('click', '[data-edit]', function (event) {

            event.preventDefault();
            event.stopPropagation();

            // botão que abre o modo de edição do registro
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

            // coluna de ordenação que recebeu o clique
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

        // verifica se o datagrid não possui nenhum item e caso 
        // positivo exibe a mensagem de nenhum item encontrado
        if ($datagrid.find('[data-display-row]').length == 0)
            showNoRecordsMessage();

        initializeEventHandlers();
    }

    initializeDatagrid();

    return self;
}

/*
 * Função global responsável por inicializar um novo datagrid a partir de um elemento html na página.
 *
 * selector: seletor css que indica o elemento html que corresponde ao novo datagrid
 * options: objeto contendo os parâmetros adicionais para a inicialização do datagrid, 
 * os valores aceitos são: onChange, onCreate, onDelete, onReload, onSave, onUpdate.
 */
window.datagrid = function (selector, options) {

    // cria uma lista temporária onde serão armazenados os objetos datagrid inicializados
    // para que ao final da execução do método esses objetos possam ser retornardos
    var initializedDatagridList = [];

    // inicializa um datagrid para cada elemento que for encontrado a partir do seletor informado
    $(selector).each(function (index, item) {

        // inicializa o datagrid e armazena o objeto retornado
        var datagridObject = new Datagrid(options, $(item));

        // adiciona o objeto criado na lista global de datagrids da página
        window.datagridList.push(datagridObject);

        // adiciona o objeto criado na lista temporária de datagrids inicializados a partir do seletor informado
        initializedDatagridList.push(datagridObject);
    });

    // caso nenhum datagrid tenha sido inicializado a partir do seletor informado retorna nulo
    if (initializedDatagridList.length == 0)
        return null;

    // caso o seletor informado aponte para mais de um elemento, é retornada uma lista contendo 
    // todos os objetos de datagrid criados, caso contrário, é retornado apenas um único objeto
    return initializedDatagridList.length > 1 ? initializedDatagridList : initializedDatagridList[0];
};

/*
 * Variável global onde ficam armazenadas todas as instâncias de datagrid existentes na página
 */
window.datagridList = [];

/*
* Extensão customizada para os objetos jQuery que obtém o objeto datagrid ao qual um determinado elemento informado
* da página esteja associado. Caso não exista nenhum objeto datagrid associado ao elemento, é retornado nulo
*/
$.fn.getDatagrid = function () {

    // objeto jQuery a partir do qual a chamada foi executada
    var $target = $(this);
    
    // inicializa a variável onde será armazenado o objeto datagrid associado ao elemento html da página
    var matchingItem = null;

    // percorre a lista global que armazena todos os objetos datagrid presentes na página, verificando
    // se algum deles corresponde ao elemento html informado na chamada da função
    for (var index in window.datagridList) {

        // se o objeto corrente da lista corresponder ao objeto datagrid que está sendo procurado, o
        // loop é interrompido e o objeto encontrado é armazenado na variável criada no passo anterior
        if (window.datagridList[index].target.is($target)) {
            matchingItem = window.datagridList[index];
            break;
        }
    }

    // retorna o valor da variável criada. Caso tenha sido encontrado um objeto de datagrid correspondente
    // ao elemento informado, este será retornado, caso contrário, será retornado null.
    return matchingItem;

};

// inicializa todos os datagrids existentes na página no momento que ela for carregada
$(document).ready(function () {

    window.datagrid('[data-datagrid]');
});