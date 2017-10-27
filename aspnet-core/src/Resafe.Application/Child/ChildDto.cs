using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resafe.Child
{
    [AutoMap(typeof(Child))]
    public class ChildDto
    {
        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
