$(document).ready(function () {
    
    window.notifier = new function () {

        var self = this;

        self.showSuccessMessage = function (message) {

            var $notificationContainer = $('[data-notification]');

            // esconde todas as notificações abertas
            $notificationContainer.hide();
            // aplica a classe de estilo default do bootstrap para notificações de sucesso
            $notificationContainer.attr('class', 'alert alert-success');
            // aplica a classe de estilo default do bootstrap para o ícone da notificação de sucesso
            $notificationContainer.find('i').attr('class', 'fa fa-check');
            // insere a mensagem da notificação
            $notificationContainer.find('[data-message]').html(message);
            // exibe o container da notificação
            showNotificationContainer();

            return true;
        };

        self.showErrorMessage = function (message) {
        
            var $notificationContainer = $('[data-notification]');

            // esconde todas as notificações abertas
            $notificationContainer.hide();
            // aplica a classe de estilo default do bootstrap para notificações de sucesso
            $notificationContainer.attr('class', 'alert alert-danger');
            // aplica a classe de estilo default do bootstrap para o ícone da notificação de sucesso
            $notificationContainer.find('i').attr('class', 'fa fa-ban');
            // insere a mensagem da notificação
            $notificationContainer.find('[data-message]').html(message);
            // exibe o container da notificação
            showNotificationContainer();

            return true;
        };
        
        function showNotificationContainer() {

            // obtém o container de notificação presente no html da tela e armazena um uma variável
            var $notificationContainer = $('[data-notification]');

            // se o container não estiver visível na tela ele então é exibido
            if (!$notificationContainer.is(':visible'))
                $notificationContainer.show();
            
            // A caixa de notificação possui um comportamento customizado ligado ao scroll da página.
            // quando o scroll da página é feito até um ponto onde a caixa de mensagem não possa mais ser 
            // vista, ela assume posição fixa na tela e passa a seguir o rolar da página. quando é feito
            // scroll novamente para o topo da página onde a caixa de mensagem estava originalmente posicionada,
            // ela deixa de possuir posição fixa e retorna à sua posição original.
            // Devido a esse comportamento, armazenamos a posição orginal do container de notificação em um atributo
            // data no elemento html, para que essa informação não se perca e possa ser recuperada quando necessário.
            // Caso a caixa de mensagem ainda não possua esse atributo salvo, ele é criado e o valor atual da posição
            // do container é setada como seu valor
            if (!$notificationContainer.data('original-position-top'))
                $notificationContainer.data('original-position-top', $notificationContainer.offset().top);

            // armazena a posição original do container em uma variável para ser utilizada posteriormente
            var originalNotificationAreaPositionTop = $notificationContainer.data('original-position-top');

            // armazena o comprimento do container de notificação em uma variável para ser utilizada posteriormente
            var originalNotificationAreaWidth = $notificationContainer.css('width');

            // obtém a posição atual da área visivel da página depois do scroll ter sido realizado
            var windowPosition = $(window).scrollTop();

            // armazena em uma variável um valor padrão em pixels para a distância entre o topo da página e o container de notificação
            var distanceFromPageTop = 25;

            var topFixedNavbarExists = $('.navbar-fixed-top').length > 0;

            if (topFixedNavbarExists) {
                distanceFromPageTop += $('.navbar-fixed-top').outerHeight();
            }

            // verifica se ao fazer o scroll na página o container de alerta atingiu ou ultrapassou o topo da área visível da página
            var notificationAreaStuckOnTop = windowPosition >= (originalNotificationAreaPositionTop - distanceFromPageTop);

            if (notificationAreaStuckOnTop) {

                if ($notificationContainer.hasClass('sticked'))
                    return false;

                var $placeholder = $('<div></div>')
                            .css('width', $notificationContainer.css('width'))
                            .css('height', $notificationContainer.css('height'))
                            .css('margin', $notificationContainer.css('margin'))
                            .attr('data-notification-container-placeholder', '');

                $notificationContainer.after($placeholder);
                
                $notificationContainer.addClass('sticked');

                $notificationContainer.css('position', 'fixed');
                $notificationContainer.css('z-index', '9999');
                $notificationContainer.css('top', distanceFromPageTop.toString() + 'px');
                $notificationContainer.css('width', originalNotificationAreaWidth);
            }
            else {

                $('[data-notification-container-placeholder]').remove();

                $notificationContainer.removeClass('sticked');
                
                $notificationContainer.css('position', 'relative');
                $notificationContainer.css('z-index', 'auto');
                $notificationContainer.css('top', 'auto');
                $notificationContainer.css('width', 'auto');
            }

            return true;
        }
        
        function initialize() {

            // cria o evento responsável por esconder o container de notificação ao clicar
            // no botão de fechamento identificado pelo atributo 'data-close' localizado dentro do container
            $('[data-notification]').on('click', '[data-close]', function (e) {

                var $closeButton = $(e.currentTarget);

                $closeButton.closest('[data-notification]').hide();
                
                $('[data-notification-container-placeholder]').remove();
            });

            $(document).scroll(function () {

                if (!$('[data-notification]').is(':visible'))
                    return false;
                
                showNotificationContainer();
            });

        }

        initialize();
    };
})