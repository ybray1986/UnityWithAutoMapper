﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.UnitOfWork
{
    public class UnitOfWorkFactory
    {
        public virtual IUnitOfWork Create()
        {
            return new UnitOfWork(new Model1());
        }

        public virtual IUnitOfWork CreateForOracle()
        {
            return new UnitOfWork(new Model1());
        }
    }
}
