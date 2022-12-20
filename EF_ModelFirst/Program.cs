using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_ModelFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            AddDataBasic();
            AddData();
            AddRangeData();
            
            DisplayData();
        }
        static void AddDataBasic()
        {
            List<User> users = new List<User>
            {
                new User{ UserName="聖殿祭司",Email="dotnetcool@gmail.com"},
                new User{ UserName="David",Email="david@gmail.com"},
                new User{ UserName="Mary",Email="mary@gmail.com"}
            };

            List<Blog> blogs = new List<Blog>
            {
                new Blog{ BlogName="DotNet 開發聖殿",Url="http://www.dotnetblog.com.tw",
                UserUserId = 1},
                new Blog{ BlogName="David's Blog",Url="http://www.davidblog.com.tw",
                UserUserId = 2},
                new Blog{ BlogName="Mary's Blog",Url="http://www.maryblog.com.tw",
                UserUserId = 3}
            };

            List<Post> posts = new List<Post>
            {
                new Post{ Title="I am 聖殿祭司",Content="I love MVC!",
                BlogBlogId = 1},
                new Post{ Title="David's Blog",Content="I love  Entity Framework!",
                BlogBlogId = 2},
                new Post{ Title="Mary's Blog",Content="I love Razor!",
                BlogBlogId = 3}
            };

            BlogContext context = new BlogContext();

            if(context.Users.Any())
            {
                Console.WriteLine("標本資料以存在,不新增資料");
                return;
            }

            //將資料加到Users實體中
            foreach (var item in users)
            {
                context.Users.Add(item);
            }
            context.SaveChanges();
            Console.WriteLine("User資料更新完成");

            //將資料加入到Blogs實體中
            foreach (var item in blogs)
            {
                context.Blogs.Add(item);
            }
            context.SaveChanges();
            Console.WriteLine("Blogs資料更新完成");

            //將資料加入到Posts實體中
            foreach (var item in posts)
            {
                context.Posts.Add(item);
            }
            context.SaveChanges();
            //也可以用苦力語法建立單一筆Entity 資料
            Post post = new Post();
            post.Title = "I am 祭司.";
            post.Content = "I love sports!";
            post.BlogBlogId = 1;
            context.Posts.Add(post);
            context.SaveChanges();
            //或用聰明一點的方法
            var easyPost = new Post { Title = "I am 溪江華.",Content = "I love coding!",
            BlogBlogId = 1};
            context.Posts.Add(easyPost);
            context.SaveChanges();

            Console.WriteLine("Posts 資料新增完成");
            Console.WriteLine("AddDataBasic()執行完成, 請按任一鍵離開....");

            Console.ReadKey();

            context.Dispose();

        }
        static void AddData()
        {
            List<User> users = new List<User>
            {
                new User{ UserName="Bob",Email="bob@gmail.com"},
                new User{ UserName="Jhonson",Email="johnson@gmail.com"},
                new User{ UserName="Lucy",Email="lucy@gmail.com"}
            };

            List<Blog> blogs = new List<Blog>
            {
                new Blog{ BlogName="Bob's Blog",Url="http://www.bobblog.com.tw",
                UserUserId = 4},
                new Blog{ BlogName="Jhonson's Blog",Url="http://www.johnsonblog.com",
                UserUserId = 5},
                new Blog{ BlogName="Lucy's Blog",Url="http://www.lucyblog.com.tw",
                UserUserId = 6}
            };

            List<Post> posts = new List<Post>
            {
                new Post{ Title="I am Tony",Content="I love Js!",
                BlogBlogId = 4},
                new Post{ Title="I am David",Content="I love jquery mobile!",
                BlogBlogId = 5},
                new Post{ Title="I am Mary",Content="I love LINQ!",
                BlogBlogId = 6}
            };

            BlogContext ctx = new BlogContext();

            if(ctx.Users.Find(4) != null)
            {
                Console.WriteLine("樣本資料已存在, 不新增資料");
                return;
            }

            users.ForEach(x => ctx.Users.Add(x));
            ctx.SaveChanges();
            Console.WriteLine("User 資料新增完成");

            blogs.ForEach(x => ctx.Blogs.Add(x));
            ctx.SaveChanges();
            Console.WriteLine("User 資料新增完成");

            posts.ForEach(x => ctx.Posts.Add(x));
            ctx.SaveChanges();
            Console.WriteLine("User 資料新增完成");

            ctx.Dispose();
            Console.WriteLine("AddData()執行完成,請按任一鍵離開");
            Console.ReadKey();

        }
        static void AddRangeData()
        {
            List<User> users = new List<User>
            {
                new User{ UserName="John",Email="John@gmail.com"},
                new User{ UserName="Tom",Email="Tom@gmail.com"},
                new User{ UserName="Rose",Email="Rose@gmail.com"}
            };

            List<Blog> blogs = new List<Blog>
            {
                new Blog{ BlogName="John's Blog",Url="http://www.Johnblog.com.tw",
                UserUserId = 7},
                new Blog{ BlogName="Tom's Blog",Url="http://www.Tomblog.com",
                UserUserId = 8},
                new Blog{ BlogName="Rose's Blog",Url="http://www.Roseblog.com.tw",
                UserUserId = 9},
                new Blog{ BlogName="Code Magic 碼魔法",Url="http://www.codeMagic.com.tw",
                UserUserId = 1}
            };

            List<Post> posts = new List<Post>
            {
                new Post{ Title="I am John",Content="I love bootstrap!",
                BlogBlogId = 7},
                new Post{ Title="I am Tom",Content="I love jquery!",
                BlogBlogId = 8},
                new Post{ Title="I am Rose",Content="I love HTML!",
                BlogBlogId = 9}
            };

            //將List加入倒entity集合
            using (var ctx = new BlogContext())
            {
                //檢查UserId為七的資料是否存在?若無新增資料
                var user = ctx.Users.Find(7);
                if(user == null)
                {
                    ctx.Users.AddRange(users);
                    ctx.SaveChanges();

                    ctx.Blogs.AddRange(blogs);
                    ctx.SaveChanges();

                    ctx.Posts.AddRange(posts);
                    ctx.SaveChanges();
                }
                else
                {
                    Console.WriteLine("樣本資料已存在,不新增資料");
                    return;
                }
            }
            Console.WriteLine("AddRange()執行完成,請按任意鍵離開");
            Console.ReadKey();
        }
        static void DisplayData()
        {
            //這裡的db是指EF的Db.Context,而非SQL SERVER 的db資料庫
            using (var db = new BlogContext())
            {
                Console.WriteLine("\n顯示所有Users: ");
                Console.WriteLine("=================");

                //以LINQ查詢

                var allUsers = from u in db.Users
                               select u;

                //////////////////////////////////////
                ///
                foreach(var item in  allUsers)
                {
                    Console.WriteLine($"{item.UserId} , {item.UserName} , {item.Email}");
                }

                Console.WriteLine("\n顯示某些條件的Users");
                Console.WriteLine("======================");
                //////////////////////////////////////

                //LINQ查詢.過濾與排序
                var filter = from u in db.Users
                             where u.UserId >= 2 && u.UserId <= 5
                             orderby u.UserName descending
                             select u;
                ////////////////////////////////////
                foreach(var item in filter)
                {
                    Console.WriteLine($"{item.UserId},{item.UserName},{item.Email}");
                }

                Console.WriteLine("\n顯示指定的Users:");
                Console.WriteLine("======================");

                var specificUsers = db.Users.ToList();
                //在ForEach()方法判斷與篩選

                specificUsers.ForEach(x =>
                {
                    if(x.UserName.Contains("祭司") || x.UserName == "Mary" ||
                    x.UserName == "John")
                    {
                        Console.WriteLine($"{x.UserId}, {x.UserName}, {x.Email}");
                    }
                });

                Console.WriteLine("\n顯示所有Blogs:");
                Console.WriteLine("=================");

                var allBlogs = from b in db.Blogs
                               select b;

                allBlogs.ToList().ForEach(b =>
                {
                    Console.WriteLine($"{b.BlogName}, {b.Url}, Owner:{b.User.UserName}");
                });

                Console.WriteLine("\n顯示所有Posts貼文");
                Console.WriteLine("=====================");

                var allPosts = from u in db.Posts
                               select u;

                allPosts.ToList().ForEach(p => {
                    Console.WriteLine($"{p.PostId},{p.Title},{p.Content}," + 
                        $"BlogBlogId : {p.BlogBlogId} , BlogName:{p.Blog.BlogName}");
                });
            }
            Console.WriteLine("");
            Console.WriteLine("請按任意鍵離開");
            Console.ReadKey();
        }
       
    }
}
