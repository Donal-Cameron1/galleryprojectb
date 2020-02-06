using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using galleryprojectb.Models;

namespace galleryprojectb.DAL
{
    public class galleryprojectbinitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<galleryprojectbcontext>
    {
        protected override void Seed(galleryprojectbcontext context)
        {
            var user = new List<User>
            {
                new User {Username = "DonalCameron", role = "user", ID = 1, Password = "password"}
            };
            user.ForEach(s => context.User.Add(s));
            context.SaveChanges();
        }
    }
}