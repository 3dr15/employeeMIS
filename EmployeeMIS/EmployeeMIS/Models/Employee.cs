using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeMIS.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UInt32 PhoneNumber { get; set; }
        
        public string DocProofLink { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }

        public string DepartmentId { get; set; }
        
        public UInt32 Salary { get; set; }

    }
}
