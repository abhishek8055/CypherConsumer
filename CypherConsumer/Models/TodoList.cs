using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CypherConsumer.Models
{
    public class TodoList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}