using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Resafe.Child
{
    public class ChildManager: DomainService
    {
        private IRepository<Child> childRepository { get; set; }


        public ChildManager(IRepository<Child> childRepository)
        {
            this.childRepository = childRepository;
        }

        public List<Child> GetChildren()
        {
            return childRepository.GetAll().ToList();
        }
    }
}
