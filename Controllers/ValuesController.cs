using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace blob_demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        
        [HttpGet("createFile")]
        public IActionResult CreateFile([FromQuery]string[] logs)
        {
            try
            {
                var sb = new StringBuilder();

                logs.ToList().ForEach(log =>
                {
                    sb.AppendLine($"{log},");
                });


                var content = ObjectToByteArray(sb.ToString());

                return File(content, "text/csv");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }



    }
}
