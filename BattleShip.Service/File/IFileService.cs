using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Service.File
{
    public interface IFileService
    {
        string GetFileContent(string fileName);
        void SetFileContent(string fileName, string content);
    }
}
