using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SvgManipulationLogic;
using Newtonsoft.Json;
using SvgServiceApi.Logic;

namespace SvgServiceApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SvgGenerationController : ControllerBase
    {
        public string test() 
        {
            return "Hello World";
        }

        [HttpPost]
        public string start([FromBody]Models.HandRequest hand) 
        {
            //var hand = JsonConvert.DeserializeObject<Models.HandRequest>(handData);
            hand = new Models.HandRequest()
            {
                MaxWidth = 80,
                WidthAtHigh = 70,
                WidthAtLow = 50,
                Length = 250,
                ConductorRegions = new List<Region>() {
                    new Region(new Rulyotano.Math.Geometry.Point(40, 60), 60, 90,0,2,1),
                    new Region(new Rulyotano.Math.Geometry.Point(120, 140), 40, 60,0,2,1) }
            };
            Generator g = new Generator();
            g.GenerateTemplate1(new TemplateRequest() { 
                MaxWidth=(float)hand.MaxWidth, 
                WidthAtLow=(float)hand.WidthAtLow, 
                WidthAtHigh=(float)hand.WidthAtHigh,
                HandHeight=(float)hand.Length,
                CondutorRegions=hand.ConductorRegions});
            return payloadStructure();
        }
        [HttpPost]
        public string makeTemplate1([FromBody] Models.HandRequest hand)
        {
            Generator g = new Generator();
            g.GenerateTemplate1(new TemplateRequest()
            {
                MaxWidth = (float)hand.MaxWidth,
                WidthAtLow = (float)hand.WidthAtLow,
                WidthAtHigh = (float)hand.WidthAtHigh,
                HandHeight = (float)hand.Length,
                CondutorRegions = hand.ConductorRegions
            });
            ImageConverter.convert();
            return payloadStructure();
        }
        [HttpGet]
        public string payloadStructure() {
            return JsonConvert.SerializeObject(new Models.HandRequest()
            {
                MaxWidth = 180,
                WidthAtHigh= 150,
                WidthAtLow= 120,
                Length=250,
                ConductorRegions = new List<Region>() { 
                    new Region(new Rulyotano.Math.Geometry.Point(40, 60), 60, 90,0,3,1),
                    new Region(new Rulyotano.Math.Geometry.Point(120, 140), 40, 60,1,2,1) }
            });
        }
    }
}
