$(document).ready(function () {

    $(document).on('click', '[data-form-content] [data-submit]', function(e) {

        e.preventDefault();

        var $formContent = $(e.currentTarget).closest('[data-form-content]');

        submitForm($formContent);
    });
    
    function submitForm($formContent) {

        $formContent.wrap('<form></form>');

        var $form = $formContent.closest('form');

        $form.validate();
        
        if (!$form.valid())
            return false;

        var url = $formContent.data('form-content');

        $.post(url, collectFormData($formContent), function (responseData, responseResult, responseMetaData) {

            $formContent.unwrap();

            // Checa o status do resultado da operação enviada nos headers do response
            var operationResultStatus = responseMetaData.getResponseHeader('OperationResultStatus');

            // Obtém a mensagem de resultado da operação enviada nos headers do response
            var operationResultMessage = responseMetaData.getResponseHeader('OperationResultMessage');

            $formContent.replace(responseData);
            
            // Verifica se houve erro no cadastro do novo item
            if (operationResultStatus == "Failed") {
                
                // Exibe a mensagem de erro com o erro que impediu que a operação fosse concluída com sucesso
                notifier.showErrorMessage(operationResultMessage);
                
                return false;
            }

            // exibe a mensagem de sucesso retornada pelo servidor
            notifier.showSuccessMessage(operationResultMessage);

            return true;

        }).fail(function (responseMetaData, responseStatus, errorThrown) {

            console.log(responseStatus + ' - ' + errorThrown);
            console.log(responseMetaData);
        });
    }
    
    window.collectFormData = function($formContainer, formData) {

        if (!formData)
            formData = {};

        // O helper CheckBoxFor do ASP.NET quando processado em HTML, gera 2 inputs, um checkbox
        // e um input hidden, para que seu processo de serialização de formulários funcione corretamente.
        // Por conta desse comportamento, primeiro são lidos os valores apenas dos inputs que não são checkboxes
        // para que só então em seguida esses possam ser lidos, para que o valor lido do campo checkbox não seja
        // sobrescrito com o valor do input hidden de mesmo nome
        $formContainer.find('input:not(:checkbox), select').each(function(index, field) {

            var $field = $(field);

            formData[$field.attr('name')] = $field.val();
        });

        $formContainer.find('input[type="checkbox"]').each(function(index, field) {

            var $field = $(field);

            formData[$field.attr('name')] = $field.is(':checked');
        });

        return formData;
    };
});