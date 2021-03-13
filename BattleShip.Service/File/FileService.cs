using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BattleShip.Service.File
{
    public class FileService : IFileService
    {
        public string GetFileContent(string fileName)
        {
            return System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName));
        }

        public void SetFileContent(string fileName, string content)
        {
            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName), content);
        }
    }
}
