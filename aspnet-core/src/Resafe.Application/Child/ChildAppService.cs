using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Resafe.Child
{
    public class ChildAppService: ResafeAppServiceBase
    {

        private ChildManager childManager { get; set; }

        public ChildAppService(ChildManager childManager)
        {
            this.childManager = childManager;
        }

        public List<ChildDto> GetChildren() => 
            childManager.GetChildren().Select(c => ObjectMapper.Map<ChildDto>(c)).ToList();

    }
}
