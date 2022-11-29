using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace DataLayer
{
    public interface IDataRepository
    {
        public List<Customer> GetTop10Customers();
        public List<Customer> GetTop10Customers(string title);
    }
}
