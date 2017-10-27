using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resafe.Child
{
    public class Child: Entity
    {
        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
