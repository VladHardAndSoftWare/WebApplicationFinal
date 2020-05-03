using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using WebApplicationFinal.Data.Models;
using System.Collections.Generic;
using System;

namespace SocialApp.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, DateTime OrderDate, List<ShopCartItem> ShopCart)
        {
            string message0 = "<body style='background - color:#e2e1e0;font-family: Open Sans, sans-serif;font-size:100%;font-weight:400;line-height:1.4;color:#000;'><table style = 'max-width:670px;margin:auto;background-color:#fff;padding:25px;-webkit-border-radius:3px;-moz-border-radius:3px;border-radius:3px;-webkit-box-shadow:0 1px 3px rgba(0,0,0,.12),0 1px 2px rgba(0,0,0,.24);-moz-box-shadow:0 1px 3px rgba(0,0,0,.12),0 1px 2px rgba(0,0,0,.24);box-shadow:0 1px 3px rgba(0,0,0,.12),0 1px 2px rgba(0,0,0,.24); border-top: solid 10px green;'><thead><tr><th style = 'text-align:left;'><img style = 'width: 50px; height: 50px;' src = 'https://sun9-44.userapi.com/c857528/v857528575/1c438c/jKkQZqA5c4w.jpg' alt = 'bachana tours'> Енот и Панда</th><th style = 'text-align:right;font-weight:400;'>" +
                OrderDate
                + "</th></tr></thead><tbody><tr><td colspan = '2' style = 'font-size:14px;padding:40px 15px 0 15px;'>Здравствуйте!<br><br>Благодарим Вас за покупку в магазине «Енот и Панда». Мы рады, что вы выбрали нас.</td></tr><tr><td colspan = '2' style = 'font-size:20px;padding:30px 15px 0 15px; text-align:center'><b> Ваш заказ </b></td></tr><tr><td colspan = '2' style = 'padding:20px 15px 0 15px;'><table width = '100%' cellpadding = '5'>";
            string StringOrder = "";
            foreach (var el in ShopCart)
            {
                StringOrder += "<tr>"
                    + "<td valign = 'top' width = '15%'><img style = 'height: 100px; width: 90px; object-fit: cover' src ='" + el.car.img + "'/></td>"
                    + "<td valign = 'top' width = '75%'>" + el.car.Name + "<br>"
                    + "Колличество:" + el.Quantity + "<br>"
                    + "Цена:" + el.price + "₽<br></td >";  
            }
            string message1 = "</table></td></tr><tr><td colspan = '2' style = 'font-size:14px;padding:20px 15px 0 15px;'><p>Оплатить заказ можно на карту Сбербанка 5469 5500 5948 0977.Получатель: АнисимоваИрина Викторовна.Возможно, вам удобнее оплатить по номеру телефона, к которому привязана карта: +7(900)646 - 17 - 08.Обратите внимание на то, что при переводе возможна комиссия. <b> Важно:</b> не пишите ничего в комментариях к оплате.</p>"
            + "<p>Произвести оплату необходимо в течение3 календарных дней.  Заказ, не оплаченный в течение этого времени, будет автоматически отменен.</p><p>Вашу посылку мы упакуем и отправим в течение 3 - 5 рабочих дней после получения оплаты. Заказным письмам/ бандеролям присваивается номер отправления(почтовый идентификатор), который мы сообщаем после отправки заказа. По нему Вы сможете отследить весь путь до вашего почтового отделения.</p><p>Простые письма отправляются без почтового идентификатора, их дорогу отследить нельзя.Если вами выбрана доставка заказа простым письмом, то мы не несем ответственности за сохранность этого конверта в процессе транспортировки!Почтовые ящики могут протекать, почтальоны могут что - то помять, и мы не можем предсказать судьбу конверта заранее, мы только надеемся на добросовестность работы почты.</p>"
            + "<p> Если вы выбрали самовывоз, мы свяжемся с вами после того, как заказ будет собран и уточним время встречи и способ оплаты. Такой заказ можно оплатить наличными или банковской картой.</p><p> Спасибо, за ваш заказ, и хорошего дня! </p><p>Ваши Енот и Панда </p></td></tr></tbody>";
                var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Енот и Панда", "enotwithpanda@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message0 + StringOrder + message1
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 25, false);
                await client.AuthenticateAsync("enotwithpanda@yandex.ru", "22042017");
                await client.SendAsync(emailMessage);


                await client.DisconnectAsync(true);
            }
        }
    }
}