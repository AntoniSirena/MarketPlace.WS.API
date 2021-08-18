using JS.Base.WS.API.Base;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Request;
using JS.Base.WS.API.DTO.Request.Domain;
using JS.Base.WS.API.DTO.Response.Domain.FreeMarket;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Models.Domain;
using JS.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using static JS.Base.WS.API.Global.Constants;

namespace JS.Base.WS.API.Controllers.Domain.FreeMarket
{

    [RoutePrefix("api/market")]
    [Authorize]
    public class MarketController : ApiController
    {

        private MyDBcontext db;
        private Response response;

        private long currentUserId = CurrentUser.GetId();

        public MarketController()
        {
            db = new MyDBcontext();
            response = new Response();
        }


        [HttpGet]
        [Route("GetById")]
        public MarketDTO GetById(long id)
        {
            var result = db.Markets.Where(y => y.Id == id && y.IsActive == true).Select(x => new MarketDTO()
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                CurrencyId = x.CurrencyId,
                Currency = x.Currency.ISO_Code,
                MarketTypeId = x.MarketTypeId,
                MarketType = x.MarketType.Description,
                ConditionId = x.ConditionId,
                Condition = x.ArticleCondition.Description,
                CategoryId = x.CategoryId,
                Category = x.Category.Description,
                SubCategoryId = x.SubCategoryId,
                SubCategory = x.SubCategory.Description,
                Ubication = x.Ubication,
                Description = x.Description,
                PhoneNumber = x.PhoneNumber,
                Img = x.Img,
                ImgPath = x.ImgPath,
                ContenTypeShort = x.ContenTypeShort,
                ContenTypeLong = x.ContenTypeLong,
                CreationDate = x.CreationDate,
                ProductTypeId = x.ProductTypeId,
                ProductType = x.ProductType.Description,
                UseStock = x.UseStock,
                Stock = x.Stock,
                MinQuantity = x.MinQuantity,
                MaxQuantity = x.MaxQuantity,
                CreationTime = x.CreationTime,
                CreatorUserId = x.CreatorUserId,
                LastModificationTime = x.LastModificationTime,
                LastModifierUserId = x.LastModifierUserId,
                DeletionTime = x.DeletionTime,
                DeleterUserId = x.DeleterUserId,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted,

            }).FirstOrDefault();

            result.Img = string.Empty;

            return result;
        }


        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<MarketDTO> GetAll()
        {
            var result = new List<MarketDTO>();

            var userRole = db.UserRoles.Where(x => x.UserId == currentUserId).FirstOrDefault();

            string[] allowViewAllMarketsByRoles = ConfigurationParameter.AllowViewAllMarketsByRoles.Split(',');

            if (allowViewAllMarketsByRoles.Contains(userRole.Role.ShortName))
            {
                result = db.Markets.Where(y => y.IsActive == true).Select(x => new MarketDTO()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                    CurrencyId = x.CurrencyId,
                    Currency = x.Currency.ISO_Code,
                    MarketTypeId = x.MarketTypeId,
                    MarketType = x.MarketType.Description,
                    ConditionId = x.ConditionId,
                    Condition = x.ArticleCondition.Description,
                    CategoryId = x.CategoryId,
                    Category = x.Category.Description,
                    SubCategoryId = x.SubCategoryId,
                    SubCategory = x.SubCategory.Description,
                    Ubication = x.Ubication,
                    Description = x.Description,
                    PhoneNumber = x.PhoneNumber,
                    Img = x.Img,
                    ImgPath = x.ImgPath,
                    ContenTypeShort = x.ContenTypeShort,
                    ContenTypeLong = x.ContenTypeLong,
                    CreationDate = x.CreationDate,
                    ProductTypeId = x.ProductTypeId,
                    ProductType = x.ProductType.Description,
                    UseStock = x.UseStock,
                    Stock = x.Stock,
                    MinQuantity = x.MinQuantity,
                    MaxQuantity = x.MaxQuantity,
                    CreationTime = x.CreationTime,
                    CreatorUserId = x.CreatorUserId,
                    LastModificationTime = x.LastModificationTime,
                    LastModifierUserId = x.LastModifierUserId,
                    DeletionTime = x.DeletionTime,
                    DeleterUserId = x.DeleterUserId,
                    IsActive = x.IsActive,
                    IsDeleted = x.IsDeleted,

                }).OrderByDescending(y => y.Id).ToList();

            }
            else
            {
                result = db.Markets.Where(c => c.CreatorUserId == currentUserId && c.IsActive == true).Select(x => new MarketDTO()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                    CurrencyId = x.CurrencyId,
                    Currency = x.Currency.ISO_Code,
                    MarketTypeId = x.MarketTypeId,
                    MarketType = x.MarketType.Description,
                    ConditionId = x.ConditionId,
                    Condition = x.ArticleCondition.Description,
                    CategoryId = x.CategoryId,
                    Category = x.Category.Description,
                    SubCategoryId = x.SubCategoryId,
                    SubCategory = x.SubCategory.Description,
                    Ubication = x.Ubication,
                    Description = x.Description,
                    PhoneNumber = x.PhoneNumber,
                    Img = x.Img,
                    ImgPath = x.ImgPath,
                    ContenTypeShort = x.ContenTypeShort,
                    ContenTypeLong = x.ContenTypeLong,
                    CreationDate = x.CreationDate,
                    ProductTypeId = x.ProductTypeId,
                    ProductType = x.ProductType.Description,
                    UseStock = x.UseStock,
                    Stock = x.Stock,
                    MinQuantity = x.MinQuantity,
                    MaxQuantity = x.MaxQuantity,
                    CreationTime = x.CreationTime,
                    CreatorUserId = x.CreatorUserId,
                    LastModificationTime = x.LastModificationTime,
                    LastModifierUserId = x.LastModifierUserId,
                    DeletionTime = x.DeletionTime,
                    DeleterUserId = x.DeleterUserId,
                    IsActive = x.IsActive,
                    IsDeleted = x.IsDeleted,

                }).OrderByDescending(y => y.Id).ToList();

            }

            return result;
        }


        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create([FromBody] RequestMarketDTO request)
        {

            if (string.IsNullOrEmpty(request.Market.Img))
            {
                response.Code = "400";
                response.Message = "Estimado usuario es necesario subir una imagen para la portada de la publicación";

                return Ok(response);
            }

            int maximumImgQuantityMarketDetail = Convert.ToInt32(ConfigurationParameter.MaximumImgQuantityMarketDetail);
            if (request.ImgDetails.Count() > maximumImgQuantityMarketDetail)
            {
                response.Code = "400";
                response.Message = string.Concat("No puedes cargar más de ", maximumImgQuantityMarketDetail.ToString(), " fotos para el detalle, favor ajuste la cantidad");

                return Ok(response);
            }


            var fileTypeAlloweds = ConfigurationParameter.ImgTypeAllowed.Split(',');

            string root = ConfigurationParameter.MarketImgDirectory;
            string[] arrayImgBase64 = request.Market.Img.Split(',');
            string imgBase64 = arrayImgBase64[arrayImgBase64.Length - 1];

            string[] splitName1 = arrayImgBase64[0].Split('/');
            string[] splitName2 = splitName1[1].Split(';');
            string contentType = splitName2[0];

            //Validate contentType
            if (!fileTypeAlloweds.Contains(contentType))
            {
                response.Code = "400";
                response.Message = "El tipo de imagen de la portada que intenta subir es desconocido, favor reemplacé la misma";

                return Ok(response);
            }

            if (request.ImgDetails.Count > 0)
            {
                int count = 0;
                foreach (var item in request.ImgDetails)
                {
                    count = count + 1;
                    string[] imgType = item.type.Split('/');
                    if (!fileTypeAlloweds.Contains(imgType[imgType.Length - 1]))
                    {
                        response.Code = "400";
                        response.Message = string.Concat("La imagen número ", count.ToString(), " de la lista que intenta subir es desconocida, favor reemplacé la misma");

                        return Ok(response);
                    }
                }
            }


            request.Market.Img = string.Empty;
            request.Market.ImgPath = string.Empty;
            request.Market.ContenTypeShort = contentType;
            request.Market.ContenTypeLong = arrayImgBase64[0];
            request.Market.CreationDate = DateTime.Now.ToString("dd/MM/yyyy");
            request.Market.CreationTime = DateTime.Now;
            request.Market.CreatorUserId = currentUserId;
            request.Market.IsActive = true;
            request.Market.TitleFormatted = RemoveAccents(request.Market.Title);

            var marketResponse = db.Markets.Add(request.Market);
            db.SaveChanges();


            //Save img
            var guid = Guid.NewGuid();
            var fileName = string.Concat("Market_img_portada_", guid);
            var filePath = Path.Combine(root, fileName) + "." + contentType;

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllBytes(filePath, Convert.FromBase64String(imgBase64));

            //update Novelty
            marketResponse.ImgPath = filePath;
            db.SaveChanges();


            //Save img details
            #region Save img details

            if (request.ImgDetails.Count > 0)
            {
                foreach (var item in request.ImgDetails)
                {
                    string[] imgShortType = item.type.Split('/');
                    string[] imgLongType = item.base64.Split(',');
                    string _imgBase64 = imgLongType[imgLongType.Length - 1];

                    var _guid = Guid.NewGuid();
                    var _fileName = string.Concat("Market_img_detail_", _guid);
                    var _filePath = Path.Combine(root, _fileName) + "." + imgShortType[imgShortType.Length - 1];

                    if (File.Exists(_filePath))
                    {
                        File.Delete(_filePath);
                    }

                    File.WriteAllBytes(_filePath, Convert.FromBase64String(_imgBase64));

                    var marketDetailRequest = new MarketImgDetail()
                    {
                        MarketId = marketResponse.Id,
                        ImgPath = _filePath,
                        ContenTypeShort = imgShortType[imgShortType.Length - 1],
                        ContenTypeLong = imgLongType[0],
                        CreatorUserId = currentUserId,
                        CreationTime = DateTime.Now,
                        IsActive = true,
                    };

                    db.MarketImgDetails.Add(marketDetailRequest);
                    db.SaveChanges();
                }
            }

            #endregion


            response.Data = new { Id = marketResponse.Id };
            response.Message = "Artículo creado con éxito";

            return Ok(response);
        }


        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update([FromBody] Market request)
        {

            if (string.IsNullOrEmpty(request.Img))
            {
                if (!File.Exists(request.ImgPath))
                {
                    response.Code = "400";
                    response.Message = "Estimado usuario es necesario subir una imagen para la portada de la publicación";

                    return Ok(response);
                }

                request.Img = string.Concat(request.ContenTypeLong, ",", JS_File.GetStrigBase64(request.ImgPath));

            }

            var fileTypeAlloweds = ConfigurationParameter.ImgTypeAllowed.Split(',');

            string root = string.Empty;
            string fileName = string.Empty;
            string filePath = string.Empty;

            string[] arrayImgBase64 = request.Img.Split(',');
            string imgBase64 = arrayImgBase64[arrayImgBase64.Length - 1];

            string[] splitName1 = arrayImgBase64[0].Split('/');
            string[] splitName2 = splitName1[1].Split(';');
            string contentType = splitName2[0];

            //Validate contentType
            if (!fileTypeAlloweds.Contains(contentType))
            {
                response.Code = InternalResponseCodeError.Code324;
                response.Message = InternalResponseCodeError.Message324;

                return Ok(response);
            }

            request.Img = string.Empty;
            request.LastModifierUserId = currentUserId;
            request.LastModificationTime = DateTime.Now;
            request.IsActive = true;
            request.TitleFormatted = RemoveAccents(request.Title);

            db.Entry(request).State = EntityState.Modified;
            db.SaveChanges();

            //Delete old image
            if (File.Exists(request.ImgPath))
            {
                File.Delete(request.ImgPath);
            }

            //Create new path
            if (!string.IsNullOrEmpty(request.ImgPath))
            {
                var guid = Guid.NewGuid();
                root = ConfigurationParameter.MarketImgDirectory;
                fileName = string.Concat("Market_img_portada_", guid);
                filePath = Path.Combine(root, fileName) + "." + contentType;
                request.ImgPath = filePath;

                var market = db.Markets.Where(x => x.Id == request.Id).FirstOrDefault();
                market.ImgPath = filePath;
                db.SaveChanges();

                File.WriteAllBytes(request.ImgPath, Convert.FromBase64String(imgBase64));
            }

            response.Message = "Artículo actualizado con éxito";

            return Ok(response);
        }


        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(long Id)
        {
            var result = db.Markets.Where(x => x.Id == Id & x.IsActive == true).FirstOrDefault();

            if (result == null)
            {
                return NotFound();
            }

            result.IsActive = false;
            result.IsDeleted = true;
            result.DeletionTime = DateTime.Now;
            result.DeleterUserId = currentUserId;
            db.SaveChanges();

            response.Message = "Artículo eliminado con éxito";

            return Ok(response);
        }


        [HttpGet]
        [Route("GetArticles")]
        public ArticleData GetArticles(string marketTypeShortName, int categoryId = 0, int subCategoryId = 0, string inputStr = "", int page = 1)
        {
            var result = new ArticleData();
            var articles = new List<Article>();

            int pagination = page * Convert.ToInt32(ConfigurationParameter.ItemsByPageMarkeetPlace);

            var marketType = db.MarketTypes.Where(x => x.ShortName == marketTypeShortName).FirstOrDefault();

            if (categoryId > 0 || subCategoryId > 0)
            {
                inputStr = string.Empty;
            }

            if (categoryId > 0 || subCategoryId > 0)
            {
                if (categoryId > 0 && subCategoryId == 0)
                {
                    articles = db.Markets.Where(x => x.MarketTypeId == marketType.Id
                        && x.CategoryId == categoryId
                        && x.IsActive == true)
                                .Select(y => new Article()
                                {
                                    Id = y.Id,
                                    Title = y.Title,
                                    Price = y.Price,
                                    CurrencyCode = y.Currency.ISO_Code,
                                    Condition = y.ArticleCondition.Description,
                                    Ubication = y.Ubication,
                                    Description = y.Description,
                                    PhoneNumber = y.PhoneNumber.ToString(),
                                    CreationDate = y.CreationDate,
                                    ProductType = y.ProductType.Description,
                                    UseStock = y.UseStock,
                                    Stock = y.Stock,
                                    MinQuantity = y.MinQuantity,
                                    MaxQuantity = y.MaxQuantity,
                                    ImgDetail = db.MarketImgDetails.Where(x => x.MarketId == y.Id).Select(z => new ImgDetail()
                                    {
                                        Id = z.Id,
                                    }).ToList(),
                                })
                                .OrderByDescending(x => x.Id)
                                .ToList();

                    result.Article = articles.Take(pagination).ToList();
                    result.TotalRecord = articles.Count();
                    result.TotalRecordByPage = result.Article.Count();
                    result.PageNumber = page;

                }
                if (subCategoryId > 0 && categoryId == 0)
                {
                    articles = db.Markets.Where(x => x.MarketTypeId == marketType.Id
                        && x.SubCategoryId == subCategoryId
                        && x.IsActive == true)
                                .Select(y => new Article()
                                {
                                    Id = y.Id,
                                    Title = y.Title,
                                    Price = y.Price,
                                    CurrencyCode = y.Currency.ISO_Code,
                                    Condition = y.ArticleCondition.Description,
                                    Ubication = y.Ubication,
                                    Description = y.Description,
                                    PhoneNumber = y.PhoneNumber.ToString(),
                                    CreationDate = y.CreationDate,
                                    ProductType = y.ProductType.Description,
                                    UseStock = y.UseStock,
                                    Stock = y.Stock,
                                    MinQuantity = y.MinQuantity,
                                    MaxQuantity = y.MaxQuantity,
                                    ImgDetail = db.MarketImgDetails.Where(x => x.MarketId == y.Id).Select(z => new ImgDetail()
                                    {
                                        Id = z.Id,
                                    }).ToList(),
                                })
                                .OrderByDescending(x => x.Id)
                                .ToList();

                    result.Article = articles.Take(pagination).ToList();
                    result.TotalRecord = articles.Count();
                    result.TotalRecordByPage = result.Article.Count();
                    result.PageNumber = page;

                }
                if (categoryId > 0 && subCategoryId > 0)
                {
                    articles = db.Markets.Where(x => x.MarketTypeId == marketType.Id
                    && x.CategoryId == categoryId
                    && x.SubCategoryId == subCategoryId
                    && x.IsActive == true)
                            .Select(y => new Article()
                            {
                                Id = y.Id,
                                Title = y.Title,
                                Price = y.Price,
                                CurrencyCode = y.Currency.ISO_Code,
                                Condition = y.ArticleCondition.Description,
                                Ubication = y.Ubication,
                                Description = y.Description,
                                PhoneNumber = y.PhoneNumber.ToString(),
                                CreationDate = y.CreationDate,
                                ProductType = y.ProductType.Description,
                                UseStock = y.UseStock,
                                Stock = y.Stock,
                                MinQuantity = y.MinQuantity,
                                MaxQuantity = y.MaxQuantity,
                                ImgDetail = db.MarketImgDetails.Where(x => x.MarketId == y.Id).Select(z => new ImgDetail()
                                {
                                    Id = z.Id,
                                }).ToList(),
                            })
                            .OrderByDescending(x => x.Id)
                            .ToList();

                    result.Article = articles.Take(pagination).ToList();
                    result.TotalRecord = articles.Count();
                    result.TotalRecordByPage = result.Article.Count();
                    result.PageNumber = page;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(inputStr))
                {
                    inputStr = RemoveAccents(inputStr);

                    articles = db.Markets.Where(x => x.MarketTypeId == marketType.Id && x.IsActive == true
                    && (x.TitleFormatted.ToLower().Contains(inputStr.ToLower()) ||
                    x.Category.DescriptionFormatted.ToLower().Contains(inputStr) ||
                    x.SubCategory.DescriptionFormatted.ToLower().Contains(inputStr) ||
                    x.Ubication.ToLower().Contains(inputStr) ||
                    x.Price.ToString().ToLower().Contains(inputStr) ||
                    x.CreationDate.ToLower().Contains(inputStr) ||
                    x.ArticleCondition.Description.ToLower().Contains(inputStr) ||
                    x.Description.ToLower().Contains(inputStr)

                    ))

                   .Select(y => new Article()
                   {
                       Id = y.Id,
                       Title = y.Title,
                       Price = y.Price,
                       CurrencyCode = y.Currency.ISO_Code,
                       Condition = y.ArticleCondition.Description,
                       Ubication = y.Ubication,
                       Description = y.Description,
                       PhoneNumber = y.PhoneNumber.ToString(),
                       CreationDate = y.CreationDate,
                       ProductType = y.ProductType.Description,
                       UseStock = y.UseStock,
                       Stock = y.Stock,
                       MinQuantity = y.MinQuantity,
                       MaxQuantity = y.MaxQuantity,
                       ImgDetail = db.MarketImgDetails.Where(x => x.MarketId == y.Id).Select(z => new ImgDetail()
                       {
                           Id = z.Id,
                       }).ToList(),
                   })
                   .OrderByDescending(x => x.Id)
                   .ToList();

                    result.Article = articles.Take(pagination).ToList();
                    result.TotalRecord = articles.Count();
                    result.TotalRecordByPage = result.Article.Count();
                    result.PageNumber = page;
                }
                else
                {
                    articles = db.Markets.Where(x => x.MarketTypeId == marketType.Id && x.IsActive == true)
                    .Select(y => new Article()
                    {
                        Id = y.Id,
                        Title = y.Title,
                        Price = y.Price,
                        CurrencyCode = y.Currency.ISO_Code,
                        Condition = y.ArticleCondition.Description,
                        Ubication = y.Ubication,
                        Description = y.Description,
                        PhoneNumber = y.PhoneNumber.ToString(),
                        CreationDate = y.CreationDate,
                        ProductType = y.ProductType.Description,
                        UseStock = y.UseStock,
                        Stock = y.Stock,
                        MinQuantity = y.MinQuantity,
                        MaxQuantity = y.MaxQuantity,
                        ImgDetail = db.MarketImgDetails.Where(x => x.MarketId == y.Id).Select(z => new ImgDetail()
                        {
                            Id = z.Id,
                        }).ToList(),
                    })
                    .OrderByDescending(x => x.Id)
                    .ToList();

                    result.Article = articles.Take(pagination).ToList();
                    result.TotalRecord = articles.Count();
                    result.TotalRecordByPage = result.Article.Count();
                    result.PageNumber = page;
                }
            }

            return result;
        }


        [HttpGet]
        [Route("GetArticleFullData")]
        public ArticleFullData GetArticleFullData(long articleId)
        {

            var result = new ArticleFullData();

            var article = db.Markets.Where(x => x.Id == articleId && x.IsActive == true).FirstOrDefault();

            var user = db.Users.Where(x => x.Id == article.CreatorUserId).FirstOrDefault();

            result.Article = new Article()
            {
                Id = article.Id,
                Title = article.Title,
                Price = article.Price,
                CurrencyCode = article.Currency.ISO_Code,
                Condition = article.ArticleCondition.Description,
                ConditionShortName = article.ArticleCondition.ShortName,
                Ubication = article.Ubication,
                Description = article.Description,
                PhoneNumber = article.PhoneNumber.ToString(),
                CreationDate = article.CreationDate,
                ProductType = article.ProductType.Description,
                UseStock = article.UseStock,
                Stock = article.Stock,
                MinQuantity = article.MinQuantity,
                MaxQuantity = article.MaxQuantity,
            };

            result.Seller = new Seller()
            {
                Id = Convert.ToInt64(article.CreatorUserId),
                Name = string.IsNullOrEmpty(user.Person?.FullName) ? string.Concat(user.Name, ' ', user.Surname) : user.Person.FullName,
                PhoneNumber = user.PhoneNumber,
                Mail = user.EmailAddress,
            };

            return result;
        }


        [HttpGet]
        [Route("GetImageByArticleId")]
        [AllowAnonymous]
        public IHttpActionResult GetImageByArticleId(long id, int width, int height)
        {
            var article = db.Markets.Where(x => x.Id == id).FirstOrDefault();

            if (File.Exists(article.ImgPath))
            {
                byte[] file = JS_File.GetImgBytes(article.ImgPath);

                if (width > 0 || height > 0)
                {
                    MemoryStream memstr = new MemoryStream(file);
                    Image img = Image.FromStream(memstr);

                    file = JS_File.ResizeImage(img, width, height);
                }
               
                JS_File.DownloadFileImg(file);
            }

            return Ok();
        }


        [HttpGet]
        [Route("GetImageDetailByArticleId")]
        [AllowAnonymous]
        public IHttpActionResult GetImageDetailByArticleId(long id, int width, int height)
        {
            var articleDetail = db.MarketImgDetails.Where(x => x.Id == id).FirstOrDefault();

            if (File.Exists(articleDetail.ImgPath))
            {
                byte[] file = JS_File.GetImgBytes(articleDetail.ImgPath);

                if (width > 0 || height > 0)
                {
                    MemoryStream memstr = new MemoryStream(file);
                    Image img = Image.FromStream(memstr);

                    file = JS_File.ResizeImage(img, width, height);
                }

                JS_File.DownloadFileImg(file);
            }

            return Ok();
        }


        //Catalogue definition
        #region Catalogue definition

        [HttpGet]
        [Route("GetCurrencies")]
        public IEnumerable<CurrencyDTO> GetCurrencies()
        {
            var result = db.Currencies.Where(y => y.IsActive == true).Select(x => new CurrencyDTO()
            {
                Id = x.Id,
                ShortName = x.ISO_Code,
                Description = x.ISO_Currency,
            }).OrderBy(x => x.Description).ToList();

            return result;
        }


        [HttpGet]
        [Route("GetMarketTypes")]
        public IEnumerable<MarketTypeDTO> GetMarketTypes()
        {
            var result = db.MarketTypes.Where(y => y.IsActive == true).Select(x => new MarketTypeDTO()
            {
                Id = x.Id,
                ShortName = x.ShortName,
                Description = x.Description,
            }).OrderBy(x => x.Description).ToList();

            return result;
        }

        [HttpGet]
        [Route("GetConditions")]
        public IEnumerable<ConditionDTO> GetConditions()
        {
            var result = db.ArticleConditions.Where(y => y.IsActive == true).Select(x => new ConditionDTO()
            {
                Id = x.Id,
                ShortName = x.ShortName,
                Description = x.Description,
            }).OrderBy(x => x.Description).ToList();

            return result;
        }

        [HttpGet]
        [Route("GetProductTypes")]
        public IEnumerable<ProductTypeDTO> GetProductTypes()
        {
            var result = db.ProductTypes.Where(y => y.IsActive == true).Select(x => new ProductTypeDTO()
            {
                Id = x.Id,
                ShortName = x.ShortName,
                Description = x.Description,
            }).OrderBy(x => x.Description).ToList();

            return result;
        }

        [HttpGet]
        [Route("GetCategories")]
        [AllowAnonymous]
        public IEnumerable<CategoryDTO> GetCategories()
        {
            var result = db.ArticleCategories.Where(y => y.IsActive == true).Select(x => new CategoryDTO()
            {
                Id = x.Id,
                ShortName = x.ShortName,
                Description = x.Description,
            }).OrderBy(x => x.Description).ToList();

            return result;
        }


        [HttpGet]
        [Route("GetSubCategories")]
        [AllowAnonymous]
        public IEnumerable<SubCategoryDTO> GetSubCategories(int categoryId)
        {
            var result = db.ArticleSubCategories.Where(y => y.CategoryId == categoryId && y.IsActive == true).Select(x => new SubCategoryDTO()
            {
                Id = x.Id,
                ShortName = x.ShortName,
                Description = x.Description,
            }).OrderBy(x => x.Description).ToList();

            return result;
        }

        #endregion



        private string RemoveAccents(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
