﻿using System;
using System.Collections.Generic;

namespace Fakebook.Posts.Domain.Models
{
    public class Post
    {
        public int Id { get; set; }
        private string _Content; 
        public string Picture { get; set; }
         private string _UserEmail;
        public DateTime CreatedAt { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<string> Likes { get; set; }


         public string UserEmail{
            get{
                return _UserEmail;
            }
            set{
                if(value.Contains('@')){
                    _UserEmail = value;
                }
                else{
                    throw new ArgumentException("A correct email format is required.");
                }
            }

        }


        public string Content{
            get{
                return _Content;
            }
            set{
                if(value.Length > 1)
                {
                    _Content = value;
                }
                else{
                    throw new ArgumentException("Please enter a comment.");
                }
            }
        }

        public Post(string userEmail, string content)
        {
            UserEmail = userEmail;
            Content = content;
            Comments = new HashSet<Comment>();
            Likes = new HashSet<string>();
        }
    }
}
