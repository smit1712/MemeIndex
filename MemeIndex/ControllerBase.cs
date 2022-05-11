using Microsoft.AspNetCore.Mvc;
using System;

namespace MemeIndex.Server
{
    public class ControllerBase : Controller
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public void HandleGeneralException(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
