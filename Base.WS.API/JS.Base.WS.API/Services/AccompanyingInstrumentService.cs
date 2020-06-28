using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Response.Domain;
using JS.Base.WS.API.Global;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Models.Domain;
using JS.Base.WS.API.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static JS.Base.WS.API.Global.Constants;

namespace JS.Base.WS.API.Services
{
    public class AccompanyingInstrumentService : IAccompanyingInstrumentService
    {
        private MyDBcontext db;

        public AccompanyingInstrumentService()
        {
            db = new MyDBcontext();
        }

        private long currentUserId = CurrentUser.GetId();

        public List<AccompInstRequestDto> GetAccompInstRequest()
        {
            var result = new List<AccompInstRequestDto>();

            string viewAllAccompanyingInstrumentRequests_ByRoles = ConfigurationParameter.ViewAllAccompanyingInstrumentRequests_ByRoles;

            var currentUserRole = db.UserRoles.Where(x => x.UserId == currentUserId)
                                               .Select(x => new { roleName = x.Role.ShortName })
                                               .FirstOrDefault();

            var roles = viewAllAccompanyingInstrumentRequests_ByRoles.Split(',');

            if (roles.Count() > 0)
            {
                bool validateRole = roles.Contains(currentUserRole.roleName);
                if (validateRole)
                {
                    result = (from rq in db.AccompanyingInstrumentRequests
                              join id in db.IdentificationDatas on rq.Id equals id.RequestId
                              where rq.IsActive == true
                              select new AccompInstRequestDto
                              {
                                  Id = id.Id,
                                  RequestId = id.RequestId,
                                  DocentFullName = id.Docent.FullName,
                                  DocumentNumber = rq.Docent.DocumentNumber,
                                  Status = rq.RequestStatu.Name,
                                  StatusColour = rq.RequestStatu.Colour,
                                  OpeningDate = rq.OpeningDate.ToString(),
                                  ClosingDate = rq.ClosingDate.ToString(),
                                  AllowEdit = rq.RequestStatu.AllowEdit,
                              })
                       .OrderByDescending(x => x.Id)
                       .ToList();
                }
                else
                {
                    result = (from rq in db.AccompanyingInstrumentRequests
                              join id in db.IdentificationDatas on rq.Id equals id.RequestId
                              where rq.CreatorUserId == currentUserId && rq.IsActive == true
                              select new AccompInstRequestDto
                              {
                                  Id = id.Id,
                                  RequestId = id.RequestId,
                                  DocentFullName = id.Docent.FullName,
                                  DocumentNumber = rq.Docent.DocumentNumber,
                                  Status = rq.RequestStatu.Name,
                                  StatusColour = rq.RequestStatu.Colour,
                                  OpeningDate = rq.OpeningDate.ToString(),
                                  ClosingDate = rq.ClosingDate.ToString(),
                                  AllowEdit = rq.RequestStatu.AllowEdit,
                              })
                          .OrderByDescending(x => x.Id)
                          .ToList();
                }

            }

            return result;

        }


        public bool CreateVariables(long requestId)
        {
            bool result = false;

            var inProcessStatus = db.RequestStatus.Where(x => x.ShortName == Constants.RequestStatus.InProcess).FirstOrDefault();


            #region variable A

            var variableA = db.Variables.Where(x => x.ShortName == Varibels.A).FirstOrDefault();

            var planningRequest = new Planning()
            {
                RequestId = requestId,
                StatusId = inProcessStatus.Id,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var planning = db.Plannings.Add(planningRequest);
            db.SaveChanges();

            int areaId = db.Areas.Where(x => x.ShortName == Constants.Areas.Pending).Select(y => y.Id).FirstOrDefault();
            int indicatorId = db.Indicators.Where(x => x.ShortName == Indicators.IndicadorInicial).Select(y => y.Id).FirstOrDefault();

            foreach (var item in variableA.variableDetails)
            {
                var planningDetailRequest = new PlanningDetail()
                {
                   PlanningId = planning.Id,
                   VariableDetailId = item.Id,
                   AreaIdA = areaId,
                   IndicadorIdA = indicatorId,
                   AreaIdB = areaId,
                   IndicadorIdB = indicatorId,
                   AreaIdC = areaId,
                   IndicadorIdC = indicatorId,
                   CreationTime = DateTime.Now,
                   CreatorUserId = currentUserId,
                   IsActive = true,
                };

                var planningDetail = db.PlanningDetails.Add(planningDetailRequest);
                db.SaveChanges();
            }

            #endregion


            result = true;

            return result;
        }

        public VariableDto GetVariableByRequestId(long requestId, string variable)
        {
            var result = new VariableDto();
            variable = variable.ToUpper();
            variable = variable.Trim();

            if (variable.Equals(Varibels.A))
            {
                result = db.Plannings.Where(x => x.RequestId == requestId).Select(y => new VariableDto() {

                    Id = y.Id,
                    RequestId = y.RequestId,
                    StausId = y.StatusId,
                    StatusDescription = y.Status.Name,
                    StatusColour = y.Status.Colour,
                    VariableDescription = y.PlanningDetails.Select(z => z.VariableDetail.Variable.Description).FirstOrDefault(),
                    VariableTitle = y.PlanningDetails.Select(z => z.VariableDetail.Variable.Title).FirstOrDefault(),
                    VariableDetails = y.PlanningDetails.Select(p => new VariableDetailsDto() {

                        Id = p.Id,
                        Number = p.VariableDetail.Number,
                        Description = p.VariableDetail.Description,
                        AreaIdA = p.AreaIdA,
                        IndicadorIdA = p.IndicadorIdA,
                        AreaIdB = p.AreaIdB,
                        IndicadorIdB = p.IndicadorIdB,
                        AreaIdC = p.AreaIdC,
                        IndicadorIdC = p.IndicadorIdC,

                    }).ToList(),

                }).FirstOrDefault();
            }

            return result;
        }
    }
}