using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model.EF;
using Newtonsoft.Json;

namespace OnlineShopAPI.Controllers.Admin
{
    [RoutePrefix("api/admin/product")]
    public class ProductController : ApiController
    {
        // GET api/<controller>
        //[Route("pagination")]
        //[System.Web.Http.AcceptVerbs("GET", "POST")]
        //public IEnumerable<string> Pagination()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [Route("pagination")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public string Pagination(Model.EF.Pagination pagingEntity)
        {
            OnlineShopDbContext db = new OnlineShopDbContext();

            int currentpage = pagingEntity.pagenumber;
            pagingEntity.pagenumber -= 1;
            int per_page = pagingEntity.perpage > 1 ? pagingEntity.perpage : 16;
            bool previouspage = true;
            bool nextpage = true;
            bool firstpage = true;
            bool lastpage = true;
            int start = pagingEntity.pagenumber * per_page;

            /* Let's build the query using available data that we received form the front-end via ajax */
            List<Model.EF.Product> products = db.Products.OrderBy(p=>p.ID).Skip(start).Take(per_page).ToList();

            /* Get total items in our database */

            int total = db.Products.Where(p => p.ID != 0).Count();

            //decimal totalpage=Decimal.Divide();
            string content = "";
            if (products.Count > 0)
            {
                content += "<table class='table'>" +
                    "<thead class='thead-light'>" +
                    "<tr>" +
                    "<th>" +
                "<label class='customcheckbox m-b-20'>" +
                "<input type = 'checkbox' id='mainCheckbox' />" +
                "<span class='checkmark'></span>" +
                "</label>" +
                "</th>" +
                "<th>Name</th>" +               
                "<th>Image</th>" +
                "<th>Price</th>" +
                "<th>Promotion price</th>" +
                "<th>Quantity</th>" +
                "<th>Status</th>" +
                "<th>Action</th>" +
                "</tr>" +
                "</thead>";
                content += "<tbody>";
                foreach (Product p in products)
                {
                    content += "<tr>";
                    content += "<th>" +
                              "<label class='customcheckbox'>" +
                        "<input type = 'checkbox' class='listCheckbox' />" +
                        "<span class='checkmark'></span>" +
                        "</label>" +
                        "</th>";
                    content += "<td>" + p.Name + "</td>";                    
                    content += "<td><img src='"+ p.Images + "' height='50px'/></td>";
                    content += "<td>" + p.Price + "</td>";
                    content += "<td>" + p.PromotionPrice + "</td>";
                    content += "<td>" + p.Quantity + "</td>";
                    content += "<td>" + p.Status + "</td>";
                    content += "<td>" +
                                   "<a href='javascript:;' class='popupclass' data-id='"+JsonConvert.SerializeObject(p)+ "' data-toggle='tooltip' data-placement='top' title='Update'>" +
                                        "<i class='mdi mdi-check'></i>"+
                                    "</a> "+
                                    "<a href = '#' data-toggle='tooltip' data-placement='top' title='Delete'>"+
                                    "<i class='mdi mdi-close'></i>"+
                                    "</a>" +
                               "<a href = 'javascript:;' class='imagepopup' data-toggle='tooltip' data-placement='top' title='Image'>" +
                               "<i class='far fa-images'></i>" +
                               "</a" +
                               "</td>";
                    content += "</tr>";
                }

                content += "</tbody></table>";
            }
            



            /* Bellow is the navigation logic and view */
            decimal nop_ceil = Decimal.Divide(total, per_page);
            int totalpage = Convert.ToInt32(Math.Ceiling(nop_ceil));

            var start_loop = 1;
            var end_loop = totalpage;

            if (currentpage >= 7)
            {
                start_loop = currentpage - 3;
                if (totalpage > currentpage + 3)
                {
                    end_loop = currentpage + 3;
                }
                else if (currentpage <= totalpage && currentpage > totalpage - 6)
                {
                    start_loop = totalpage - 6;
                    end_loop = totalpage;
                }
            }
            else
            {
                if (totalpage > 7)
                {
                    end_loop = 7;
                }
            }

            string paging = "<nav aria-label='pagination example'>" +
                            "<ul class='pagination pagination-circle pg-blue mb-0'>";

            if (firstpage && currentpage > 1)
            {
                paging += "<li class='page-item enable' p='1'><a class='page-link'>First</a></li>";
            }
            else if (firstpage)
            {
                paging += "<li class='page-item disabled' p='1'><a class='page-link'>First</a></li>";
            }

            if (previouspage && currentpage > 1)
            {
                var pre = currentpage - 1;
                paging += "<li class='page-item enable' p='" + pre + "'>" +
                          "<a class='page-link' aria-label='Previous'>" +
                          "<span aria-hidden='true'>&laquo;</span>" +
                          "<span class='sr-only'>Previous</span>" +
                          "</a>" +
                          "</li>";
            }
            else if (previouspage)
            {
                paging += "<li class='page-item disabled'>" +
                          "<a class='page-link' aria-label='Previous'>" +
                          "<span aria-hidden='true'>&laquo;</span>" +
                          "<span class='sr-only'>Previous</span>" +
                          "</a>" +
                          "</li>";
            }

            for (int i = start_loop; i <= end_loop; i++)
            {
                if (currentpage == i)
                {
                    paging += "<li class='page-item active' p='"+i+"'><a class='page-link'>"+i+"</a></li>";
                }
                else
                {
                    paging += "<li class='page-item enable' p='" + i+"'><a class='page-link'>"+i+"</a></li>";
                }
            }

            if (nextpage && currentpage < totalpage)
            {
                var next = currentpage + 1;
                paging += "<li class='page-item enable' p='" + next+"'>" +
                    "<a class='page-link' aria-label='Next'>" +
                    "<span aria-hidden='true'>&raquo;</span>" +
                    "<span class='sr-only'>Next</span>" +
                    "</a>" +
                    "</li>";
            }

            if (lastpage && currentpage < totalpage)
            {
                paging += "<li class='page-item enable' p='" + totalpage+"'><a class='page-link'>Last</a></li>";
            }
            else if (lastpage)
            {
                paging += "<li class='page-item disabled' p='" + totalpage+"'><a class='page-link'>Last</a></li>";
            }

            paging += "</ul></nav>";

            #region pagnation template
            //paging = "<nav aria-label='pagination example'>" +
            //        "<ul class='pagination pagination-circle pg-blue mb-0'>" +
            //    "<li class='page-item disabled'><a class='page-link'>First</a></li>" +
            //    "<li class='page-item disabled'>" +
            //    "<a class='page-link' aria-label='Previous'>" +
            //    "<span aria-hidden='true'>&laquo;</span>" +
            //    "<span class='sr-only'>Previous</span>" +
            //    "</a></li><li class='page-item active'><a class='page-link'>1</a></li>" +
            //        "<li class='page-item'><a class='page-link'>2</a></li>" +
            //        "<li class='page-item'><a class='page-link'>3</a></li>" +
            //        "<li class='page-item'><a class='page-link'>4</a></li>" +
            //        "<li class='page-item'><a class='page-link'>5</a></li>" +
            //        "<li class='page-item'>" +
            //        "<a class='page-link' aria-label='Next'>" +
            //        "<span aria-hidden='true'>&raquo;</span>" +
            //        "<span class='sr-only'>Next</span>" +
            //        "</a>" +
            //        "</li>" +
            //        "<li class='page-item'><a class='page-link'>Last</a></li>" +
            //        "</ul>" +
            //        "</nav>"; 
            #endregion

            Hashtable hashtable = new Hashtable();
            hashtable["content"] = content;
            hashtable["paging"] = paging;
            string jsonstr = JsonConvert.SerializeObject(hashtable);
            return jsonstr;
        }
    }
}