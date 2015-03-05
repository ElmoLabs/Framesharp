$(document).ready(function () {

    $.fn.initializeEventListener = function initializeEventListener() {

        // controle que escuta o disparo do evento 'change' de outro controle
        var $control = this;

        // valor do atributo id do controle cujo evento 'change' deve ser escutado
        var listenedControlId = $control.data('listened-control-id');

        // controle que deve ser escutado
        var $listenedControl = $("[id='" + listenedControlId + "']");

        // adiciona no controle escutado, um atributo especial, identificando 
        // ele como um controle que está sob escuta de outro controle
        $listenedControl.attr('data-listened-control', listenedControlId);

        // cria um seletor jQuery para o controle escutado baseado no atributo especial que foi criado
        // no passo anterior, que será utilizando para identificar o controle na hora de assinar seu 
        // evento de change
        var listenedControlSelector = '[data-listened-control="' + listenedControlId + '"]';

        // evento que deve ser disparado quando o controle que estiver sendo escutado tiver seu evento change disparado
        var eventCallbackUrl = $control.data('event-callback-url');

        // verifica se o elemento alvo da atualização é um controle do tipo select, e caso seja, armazena
        // seu valor selecionado original, dessa maneira, caso a opção original selecionada esteja presente na versão
        // atualizada do controle, ela poderá ser restaurada automaticamente
        if ($control.is('select'))
            $control.attr('data-original-selected-value', $control.val());

        // assina o evento change do controle escutado, para que execute a chamada de callback obtida 
        // no passo anterior no momento que o controle escutado tiver seu valor modificado
        $(document).on("change", listenedControlSelector, function () {

            // objeto que contém os parâmetros do request que será feito
            var requestData = {};

            // obtém o name e o valor do controle escutado e adiciona no objeto com os parâmetros do request
            requestData[$listenedControl.attr('name')] = $listenedControl.val();

            // executa um request à url de callback definida anteriormente passando os parâmetros coletados
            $.ajax({
                url: eventCallbackUrl,
                data: requestData,
                type: 'POST',
                success: function (responseData, responseStatus, responseMetaData) {

                    // código html que foi retornado pelo servidor. Ele contém o controle que estava
                    // escutando o disparo do evento change de outro controle, já devidamente atualizado
                    var $updatedControl = $(responseData);

                    // pega o controle atualizado retornado pelo servidor e redefine o atributo que identifica qual
                    // o id do controle cujo evento change ele deve escutar, pois o valor desse atributo pode ter sido 
                    // alterado pelo servidor, fazendo com que o controle perca a ligação de escuta que possuia antes de 
                    // ser atualizado
                    $updatedControl.attr('data-listened-control-id', listenedControlId);

                    // cria um placeholder para identificar no html em que lugar o controle atualizado será inserido
                    var $updatedContentPlaceholder = $('<div></div>').attr('data-updated-content-placeholder', listenedControlId);

                    // colocar o placeholder uma posição apoós o controle antigo desatualizado
                    $control.after($updatedContentPlaceholder);

                    // remove o controle antigo desatualizado
                    $control.remove();

                    // coloca o controle atualizado após o placeholder
                    $updatedContentPlaceholder.after($updatedControl);

                    // caso o elemento alvo da atualização seja um controle de formulário, restaura o valor
                    // original do atributo name que ele possuia antes da atualização, pois este pode ter sido 
                    // modificado no servidor
                    if ($updatedControl.is('input, select'))
                        $updatedControl.attr('name', $control.attr('name'));

                    // caso o elemento atualizado seja um controle do tipo select, restaura, se possível, 
                    // o valor original selecionado antes do elemento ser atualizado
                    if ($control.attr('data-original-selected-value')) {

                        // valor selecionado original
                        var originalSelectedValue = $control.data('original-selected-value');

                        // restaura o valor selecionado original
                        $updatedControl.val(originalSelectedValue).change();

                        // se após a opção selecionada ter sido restaurada, o valor do controle continuar nulo,
                        // significa que a opção não estava presente na lista de opções do controle atualizado,
                        // nesse caso selecionamos automaticamente a primeira opção do select atualizado
                        if (!$updatedControl.val()) {
                            $updatedControl.val($updatedControl.find("option:first").val()).change();
                        }

                        // adiciona o atributo que armazena o valor selecionado original no controle atualizado
                        $updatedControl.attr('data-original-selected-value', originalSelectedValue);
                    }

                    // remove todos os placeholders dedicados ao controle atualizado
                    $('[data-updated-content-placeholder="' + listenedControlId + '"]').remove();

                    // atualiza a variável de escopo $control que representa o controle que escuta os eventos
                    // de outro controle, para a versão atualizada retornada pelo servidor
                    $control = $updatedControl;
                },
                error: function (responseMetaData, responseStatus, errorThrown) {

                    console.log(responseStatus + ' - ' + errorThrown);
                    console.log(responseMetaData);
                }
            });

        });
    };

    $.fn.initializeEventTrigger = function initializeEventTrigger() {

        // controle cujo evento change será assinado
        var $eventTriggerControl = this;

        // url que será utilizada para realizar o request ao servidor
        var eventCallbackUrl = $eventTriggerControl.data('event-callback-url');

        // id do elemento que deverá ser atualizado
        var targetControlId = $eventTriggerControl.data('target-control-id');

        // elemento que deverá ser atualizado no disparo do evento de change do controle
        var $targetControl = $("[id='" + targetControlId + "']");

        // adiciona um atributo identificador no elemento que deve ser atualizado, para que uma vez
        // que este seja atualizado, não perca o bind com o controle que disparou o evento change, caso
        // o id do elemento tenha sido modificado no servidor antes de ser devolvido no response
        $targetControl.attr('data-target-control', targetControlId);

        // verifica se o elemento alvo da atualização é um controle do tipo select, e caso seja, armazena
        // seu valor selecionado original, dessa maneira, caso a opção original selecionada esteja presente na versão
        // atualizada do controle, ela poderá ser restaurada automaticamente
        if ($targetControl.is('select'))
            $targetControl.attr('data-original-selected-value', $targetControl.val());

        $eventTriggerControl.on("change", function () {

            var requestData = {};

            requestData[$eventTriggerControl.attr('name')] = $eventTriggerControl.val();

            $.ajax({
                url: eventCallbackUrl,
                data: requestData,
                type: 'POST',
                success: function (responseData, responseStatus, responseMetaData) {

                    // versão atualizada do elemento retornada pelo servidor
                    var $updatedControl = $(responseData);

                    // restaura o atributo identificador do elemento que deve ser atualizado na sua versão
                    // atualizada trazida pelo servidor, para que quando o evento de change do controle seja
                    // disparado novamente, o elemento atualizado possa ser identificado novamente, e atualizado
                    // quantas vezes forem necessárias
                    $updatedControl.attr('data-target-control', targetControlId);

                    // cria um placeholder para identificar onde o elemnto atualizado será inserido
                    var $updatedContentPlaceholder = $('<div></div>').attr('data-updated-content-placeholder', targetControlId);

                    // insere o placeholder após a versão desatualizada do elemento na tela
                    $targetControl.after($updatedContentPlaceholder);

                    // remove o elemento desatualizado da tela
                    $targetControl.remove();

                    // insere o elemento atualizado após o placeholder
                    $updatedContentPlaceholder.after($updatedControl);

                    // caso o elemento alvo da atualização seja um controle de formulário, restaura o valor
                    // original do atributo name que ele possuia antes da atualização, pois este pode ter sido 
                    // modificado no servidor
                    if ($updatedControl.is('input, select'))
                        $updatedControl.attr('name', $targetControl.attr('name'));

                    // caso o elemento atualizado seja um controle do tipo select, restaura, se possível, 
                    // o valor original selecionado antes do elemento ser atualizado
                    if ($targetControl.attr('data-original-selected-value')) {

                        // valor selecionado original
                        var originalSelectedValue = $targetControl.data('original-selected-value');

                        // restaura o valor selecionado original
                        $updatedControl.val(originalSelectedValue).change();

                        // se após a opção selecionada ter sido restaurada, o valor do controle continuar nulo,
                        // significa que a opção não estava presente na lista de opções do controle atualizado,
                        // nesse caso selecionamos automaticamente a primeira opção do select atualizado
                        if (!$updatedControl.val()) {
                            $updatedControl.val($updatedControl.find("option:first").val()).change();
                        }

                        // adiciona o atributo que armazena o valor selecionado original no controle atualizado
                        $updatedControl.attr('data-original-selected-value', originalSelectedValue);
                    }

                    // remove o placeholder
                    $('[data-updated-content-placeholder="' + targetControlId + '"]').remove();

                    $targetControl = $updatedControl;
                },
                error: function (responseMetaData, responseStatus, errorThrown) {

                    console.log(responseStatus + ' - ' + errorThrown);
                    console.log(responseMetaData);
                }
            });

        });
    };

    initializeEventListeners();
    initializeEventTriggers();
});

function initializeEventListeners($scope) {

    if (!$scope)
        $scope = document;

    $scope = $($scope);

    var $eventListenerControlList = $scope.find('[data-event-listener]');

    $eventListenerControlList.each(function (index, $control) {

        $control = $($control);

        $control.initializeEventListener();
    });
};

function initializeEventTriggers($scope) {

    if (!$scope)
        $scope = document;

    $scope = $($scope);

    var $eventTriggerControlList = $scope.find('[data-event-trigger]');

    $eventTriggerControlList.each(function (index, $control) {

        $control = $($control);

        $control.initializeEventTrigger();
    });
};