using System;
using System.Threading.Tasks;

namespace Demo.Core.Services
{
    public interface IUserInteraction
    {
        /// <summary>
        /// Показывает пользователю простой диалог с необходимым сообщением, заголовком и названием кнопки
        /// NOTE: платформозависимый метод (отдельная реализация для каждой платформы)
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="done">Действие, которое необходимо выполнить после того как пользователь нажмет на кнопку.</param>
        /// <param name="title">Заголовок.</param>
        /// <param name="okButton">Текст кнопки подтверждения.</param>
        void Alert(string message, Action done = null, string title = "", string okButton = "OK");
    }
}

