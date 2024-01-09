﻿using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainServices
{
    public interface ICanteenRepository
    {
        IEnumerable<Student> GetAll();
    }
}
