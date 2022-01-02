using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Cms;

namespace MailService.Services
{
    public class MailGatewayOptions
    {
        private const int defaultPort = 25;

        /// <summary>
        /// IP адрес SMTP сервера
        /// </summary>
        /// <value></value>
        public string SmtpServer { get; init; } = null!;

        /// <summary>
        /// Порт сервера
        /// </summary>
        /// <value>по умолчанию - 25</value>
        public int Port { get; init; } = defaultPort;

        /// <summary>
        /// Имя пользователя SMTP сервера
        /// </summary>
        /// <value></value>
        public string UserName { get; init; } = null!;

        /// <summary>
        /// Пароль пользователя SMTP сервера
        /// </summary>
        /// <value></value>
        public string Password { get; init; } = null!;

        /// <summary>
        /// Отправитель, будет подставляться в поле From
        /// </summary>
        /// <value></value>
        public string Sender { get; init; } = null!;
    }
}