using System;
using System.Collections.Generic;
using Demo.API.Models.Beacons;

namespace Demo.Core.Services
{
    public interface IBeaconService
    {
        /// <summary>
        /// Запускает поиск маячков
        /// NOTE: платформозависимый метод (отдельная реализация для каждой платформы)
        /// </summary>
        /// <param name="beacons">Список маячков.</param>
        void Start (List<BeaconModel> beacons);

        /// <summary>
        /// Останавливает поиск маячков
        /// </summary>
        void Stop ();
    }
}

