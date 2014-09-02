using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyPortal.Models
{
    public class MainPageObjects
    {
        public MainPageObjects(List<Post> postsIn, List<Vote> votesIn)
        {
            posts = postsIn;
            votes = votesIn;
        }
        public List<Post> posts { get; set; }
        public List<Vote> votes { get; set; }
    }
}