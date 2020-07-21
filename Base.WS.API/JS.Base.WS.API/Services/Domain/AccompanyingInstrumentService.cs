using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Response.Domain;
using JS.Base.WS.API.Global;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Models.Domain;
using JS.Base.WS.API.Models.Domain.AccompanyingInstrument;
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
            int areaId = db.Areas.Where(x => x.ShortName == Constants.Areas.Pending).Select(y => y.Id).FirstOrDefault();
            int indicatorId = db.Indicators.Where(x => x.ShortName == Indicators.IndicadorInicial).Select(y => y.Id).FirstOrDefault();

            //vairable A
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


            //Variable B
            #region variable B

            var variableB = db.Variables.Where(x => x.ShortName == Varibels.B).FirstOrDefault();

            var contentDomainRequest = new ContentDomain()
            {
                RequestId = requestId,
                StatusId = inProcessStatus.Id,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var contentDomain = db.ContentDomains.Add(contentDomainRequest);
            db.SaveChanges();

            foreach (var item in variableB.variableDetails)
            {
                var contentDomainDetailRequest = new ContentDomainDetail()
                {
                    ContentDomainId = contentDomain.Id,
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

                var contentDomainDetail = db.ContentDomainDetails.Add(contentDomainDetailRequest);
                db.SaveChanges();
            }

            #endregion


            //Variable C
            #region variable C

            var variableC = db.Variables.Where(x => x.ShortName == Varibels.C).FirstOrDefault();

            var strategyActivityRequest = new StrategyActivity()
            {
                RequestId = requestId,
                StatusId = inProcessStatus.Id,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var strategyActivity = db.StrategyActivities.Add(strategyActivityRequest);
            db.SaveChanges();

            foreach (var item in variableC.variableDetails)
            {
                var strategyActivityDetailRequest = new StrategyActivityDetail()
                {
                    StrategyActivityId = strategyActivity.Id,
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

                var strategyActivityDetail = db.StrategyActivityDetails.Add(strategyActivityDetailRequest);
                db.SaveChanges();
            }

            #endregion


            //Variable D
            #region variable D

            var variableD = db.Variables.Where(x => x.ShortName == Varibels.D).FirstOrDefault();

            var pedagogicalResourceRequest = new PedagogicalResource()
            {
                RequestId = requestId,
                StatusId = inProcessStatus.Id,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var pedagogicalResource = db.PedagogicalResources.Add(pedagogicalResourceRequest);
            db.SaveChanges();

            foreach (var item in variableD.variableDetails)
            {
                var pedagogicalResourceDetailRequest = new PedagogicalResourceDetail()
                {
                    PedagogicalResourceId = pedagogicalResource.Id,
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

                var pedagogicalResourceDetail = db.PedagogicalResourceDetails.Add(pedagogicalResourceDetailRequest);
                db.SaveChanges();
            }

            #endregion


            //Variable E
            #region variable E

            var variableE = db.Variables.Where(x => x.ShortName == Varibels.E).FirstOrDefault();

            var evaluationProcessRequest = new EvaluationProcess()
            {
                RequestId = requestId,
                StatusId = inProcessStatus.Id,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var evaluationProcess = db.EvaluationProcesses.Add(evaluationProcessRequest);
            db.SaveChanges();

            foreach (var item in variableE.variableDetails)
            {
                var evaluationProcessDetailRequest = new EvaluationProcessDetail()
                {
                    EvaluationProcessId = evaluationProcess.Id,
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

                var evaluationProcessDetail = db.EvaluationProcessDetails.Add(evaluationProcessDetailRequest);
                db.SaveChanges();
            }

            #endregion


            //Variable F
            #region variable F

            var variableF = db.Variables.Where(x => x.ShortName == Varibels.F).FirstOrDefault();

            var classroomClimateRequest = new ClassroomClimate()
            {
                RequestId = requestId,
                StatusId = inProcessStatus.Id,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var classroomClimate = db.ClassroomClimates.Add(classroomClimateRequest);
            db.SaveChanges();

            foreach (var item in variableF.variableDetails)
            {
                var classroomClimateDetailRequest = new ClassroomClimateDetail()
                {
                    ClassroomClimateId = classroomClimate.Id,
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

                var classroomClimateDetail = db.ClassroomClimateDetails.Add(classroomClimateDetailRequest);
                db.SaveChanges();
            }

            #endregion


            //Variable G
            #region variable G

            var variableG = db.Variables.Where(x => x.ShortName == Varibels.G).FirstOrDefault();

            var reflectionPracticeRequest = new ReflectionPractice()
            {
                RequestId = requestId,
                StatusId = inProcessStatus.Id,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var reflectionPractice = db.ReflectionPractices.Add(reflectionPracticeRequest);
            db.SaveChanges();

            foreach (var item in variableG.variableDetails)
            {
                var reflectionPracticeDetailRequest = new ReflectionPracticeDetail()
                {
                    ReflectionPracticeId = reflectionPractice.Id,
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

                var reflectionPracticeDetail = db.ReflectionPracticeDetails.Add(reflectionPracticeDetailRequest);
                db.SaveChanges();
            }

            #endregion


            //Variable H
            #region variable H

            var variableH = db.Variables.Where(x => x.ShortName == Varibels.H).FirstOrDefault();

            var relationFatherMotherRequest = new RelationFatherMother()
            {
                RequestId = requestId,
                StatusId = inProcessStatus.Id,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var relationFatherMother = db.RelationFatherMothers.Add(relationFatherMotherRequest);
            db.SaveChanges();

            foreach (var item in variableH.variableDetails)
            {
                var relationFatherMotherDetailRequest = new RelationFatherMotherDetail()
                {
                    RelationFatherMotherId = relationFatherMother.Id,
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

                var relationFatherMotherDetail = db.RelationFatherMotherDetails.Add(relationFatherMotherDetailRequest);
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

            //Variable A
            if (variable.Equals(Varibels.A))
            {
                result = db.Plannings.Where(x => x.RequestId == requestId).Select(y => new VariableDto() {

                    Id = y.Id,
                    RequestId = y.RequestId,
                    Variable = Varibels.A,
                    StausId = y.StatusId,
                    StatusDescription = y.Status.Name,
                    StatusColour = y.Status.Colour,
                    VariableDescription = y.PlanningDetails.Select(z => z.VariableDetail.Variable.Description).FirstOrDefault(),
                    VariableTitle = y.PlanningDetails.Select(z => z.VariableDetail.Variable.Title).FirstOrDefault(),
                    AreaIdA = y.PlanningDetails.Select(z => z.AreaIdA).FirstOrDefault(),
                    AreaIdB = y.PlanningDetails.Select(z => z.AreaIdB).FirstOrDefault(),
                    AreaIdC = y.PlanningDetails.Select(z => z.AreaIdC).FirstOrDefault(),
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


            //Variable B
            if (variable.Equals(Varibels.B))
            {
                result = db.ContentDomains.Where(x => x.RequestId == requestId).Select(y => new VariableDto()
                {

                    Id = y.Id,
                    RequestId = y.RequestId,
                    Variable = Varibels.B,
                    StausId = y.StatusId,
                    StatusDescription = y.Status.Name,
                    StatusColour = y.Status.Colour,
                    VariableDescription = y.ContentDomainDetails.Select(z => z.VariableDetail.Variable.Description).FirstOrDefault(),
                    VariableTitle = y.ContentDomainDetails.Select(z => z.VariableDetail.Variable.Title).FirstOrDefault(),
                    AreaIdA = y.ContentDomainDetails.Select(z => z.AreaIdA).FirstOrDefault(),
                    AreaIdB = y.ContentDomainDetails.Select(z => z.AreaIdB).FirstOrDefault(),
                    AreaIdC = y.ContentDomainDetails.Select(z => z.AreaIdC).FirstOrDefault(),
                    VariableDetails = y.ContentDomainDetails.Select(p => new VariableDetailsDto()
                    {

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


            //Variable C
            if (variable.Equals(Varibels.C))
            {
                result = db.StrategyActivities.Where(x => x.RequestId == requestId).Select(y => new VariableDto()
                {

                    Id = y.Id,
                    RequestId = y.RequestId,
                    Variable = Varibels.C,
                    StausId = y.StatusId,
                    StatusDescription = y.Status.Name,
                    StatusColour = y.Status.Colour,
                    VariableDescription = y.StrategyActivityDetails.Select(z => z.VariableDetail.Variable.Description).FirstOrDefault(),
                    VariableTitle = y.StrategyActivityDetails.Select(z => z.VariableDetail.Variable.Title).FirstOrDefault(),
                    AreaIdA = y.StrategyActivityDetails.Select(z => z.AreaIdA).FirstOrDefault(),
                    AreaIdB = y.StrategyActivityDetails.Select(z => z.AreaIdB).FirstOrDefault(),
                    AreaIdC = y.StrategyActivityDetails.Select(z => z.AreaIdC).FirstOrDefault(),
                    VariableDetails = y.StrategyActivityDetails.Select(p => new VariableDetailsDto()
                    {

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


            //Variable D
            if (variable.Equals(Varibels.D))
            {
                result = db.PedagogicalResources.Where(x => x.RequestId == requestId).Select(y => new VariableDto()
                {

                    Id = y.Id,
                    RequestId = y.RequestId,
                    Variable = Varibels.D,
                    StausId = y.StatusId,
                    StatusDescription = y.Status.Name,
                    StatusColour = y.Status.Colour,
                    VariableDescription = y.PedagogicalResourceDetails.Select(z => z.VariableDetail.Variable.Description).FirstOrDefault(),
                    VariableTitle = y.PedagogicalResourceDetails.Select(z => z.VariableDetail.Variable.Title).FirstOrDefault(),
                    AreaIdA = y.PedagogicalResourceDetails.Select(z => z.AreaIdA).FirstOrDefault(),
                    AreaIdB = y.PedagogicalResourceDetails.Select(z => z.AreaIdB).FirstOrDefault(),
                    AreaIdC = y.PedagogicalResourceDetails.Select(z => z.AreaIdC).FirstOrDefault(),
                    VariableDetails = y.PedagogicalResourceDetails.Select(p => new VariableDetailsDto()
                    {

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


            //Variable E
            if (variable.Equals(Varibels.E))
            {
                result = db.EvaluationProcesses.Where(x => x.RequestId == requestId).Select(y => new VariableDto()
                {

                    Id = y.Id,
                    RequestId = y.RequestId,
                    Variable = Varibels.E,
                    StausId = y.StatusId,
                    StatusDescription = y.Status.Name,
                    StatusColour = y.Status.Colour,
                    VariableDescription = y.EvaluationProcessDetails.Select(z => z.VariableDetail.Variable.Description).FirstOrDefault(),
                    VariableTitle = y.EvaluationProcessDetails.Select(z => z.VariableDetail.Variable.Title).FirstOrDefault(),
                    AreaIdA = y.EvaluationProcessDetails.Select(z => z.AreaIdA).FirstOrDefault(),
                    AreaIdB = y.EvaluationProcessDetails.Select(z => z.AreaIdB).FirstOrDefault(),
                    AreaIdC = y.EvaluationProcessDetails.Select(z => z.AreaIdC).FirstOrDefault(),
                    VariableDetails = y.EvaluationProcessDetails.Select(p => new VariableDetailsDto()
                    {

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


            //Variable F
            if (variable.Equals(Varibels.F))
            {
                result = db.ClassroomClimates.Where(x => x.RequestId == requestId).Select(y => new VariableDto()
                {

                    Id = y.Id,
                    RequestId = y.RequestId,
                    Variable = Varibels.F,
                    StausId = y.StatusId,
                    StatusDescription = y.Status.Name,
                    StatusColour = y.Status.Colour,
                    VariableDescription = y.ClassroomClimateDetails.Select(z => z.VariableDetail.Variable.Description).FirstOrDefault(),
                    VariableTitle = y.ClassroomClimateDetails.Select(z => z.VariableDetail.Variable.Title).FirstOrDefault(),
                    AreaIdA = y.ClassroomClimateDetails.Select(z => z.AreaIdA).FirstOrDefault(),
                    AreaIdB = y.ClassroomClimateDetails.Select(z => z.AreaIdB).FirstOrDefault(),
                    AreaIdC = y.ClassroomClimateDetails.Select(z => z.AreaIdC).FirstOrDefault(),
                    VariableDetails = y.ClassroomClimateDetails.Select(p => new VariableDetailsDto()
                    {

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


            //Variable G
            if (variable.Equals(Varibels.G))
            {
                result = db.ReflectionPractices.Where(x => x.RequestId == requestId).Select(y => new VariableDto()
                {

                    Id = y.Id,
                    RequestId = y.RequestId,
                    Variable = Varibels.G,
                    StausId = y.StatusId,
                    StatusDescription = y.Status.Name,
                    StatusColour = y.Status.Colour,
                    VariableDescription = y.ReflectionPracticeDetails.Select(z => z.VariableDetail.Variable.Description).FirstOrDefault(),
                    VariableTitle = y.ReflectionPracticeDetails.Select(z => z.VariableDetail.Variable.Title).FirstOrDefault(),
                    AreaIdA = y.ReflectionPracticeDetails.Select(z => z.AreaIdA).FirstOrDefault(),
                    AreaIdB = y.ReflectionPracticeDetails.Select(z => z.AreaIdB).FirstOrDefault(),
                    AreaIdC = y.ReflectionPracticeDetails.Select(z => z.AreaIdC).FirstOrDefault(),
                    VariableDetails = y.ReflectionPracticeDetails.Select(p => new VariableDetailsDto()
                    {

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


            //Variable H
            if (variable.Equals(Varibels.H))
            {
                result = db.RelationFatherMothers.Where(x => x.RequestId == requestId).Select(y => new VariableDto()
                {

                    Id = y.Id,
                    RequestId = y.RequestId,
                    Variable = Varibels.H,
                    StausId = y.StatusId,
                    StatusDescription = y.Status.Name,
                    StatusColour = y.Status.Colour,
                    VariableDescription = y.RelationFatherMotherDetails.Select(z => z.VariableDetail.Variable.Description).FirstOrDefault(),
                    VariableTitle = y.RelationFatherMotherDetails.Select(z => z.VariableDetail.Variable.Title).FirstOrDefault(),
                    AreaIdA = y.RelationFatherMotherDetails.Select(z => z.AreaIdA).FirstOrDefault(),
                    AreaIdB = y.RelationFatherMotherDetails.Select(z => z.AreaIdB).FirstOrDefault(),
                    AreaIdC = y.RelationFatherMotherDetails.Select(z => z.AreaIdC).FirstOrDefault(),
                    VariableDetails = y.RelationFatherMotherDetails.Select(p => new VariableDetailsDto()
                    {

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


        public bool UpdateVariable(VariableDto request)
        {
            bool result = false;

            try
            {
                //Variable A
                if (request.Variable.Equals(Varibels.A))
                {
                    var variable = db.Plannings.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

                    foreach (var item in request.VariableDetails)
                    {
                        var variableDetails = db.PlanningDetails.Where(x => x.Id == item.Id).FirstOrDefault();

                        variableDetails.AreaIdA = request.AreaIdA;
                        variableDetails.IndicadorIdA = item.IndicadorIdA;

                        variableDetails.AreaIdB = request.AreaIdB;
                        variableDetails.IndicadorIdB = item.IndicadorIdB;

                        variableDetails.AreaIdC = request.AreaIdC;
                        variableDetails.IndicadorIdC = item.IndicadorIdC;

                        variableDetails.LastModifierUserId = currentUserId;
                        variableDetails.LastModificationTime = DateTime.Now;

                       var response = db.SaveChanges();

                        result = true;
                    }
                }
                // End variable A


                //Variable B
                if (request.Variable.Equals(Varibels.B))
                {
                    var variable = db.ContentDomains.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

                    foreach (var item in request.VariableDetails)
                    {
                        var variableDetails = db.ContentDomainDetails.Where(x => x.Id == item.Id).FirstOrDefault();

                        variableDetails.AreaIdA = request.AreaIdA;
                        variableDetails.IndicadorIdA = item.IndicadorIdA;

                        variableDetails.AreaIdB = request.AreaIdB;
                        variableDetails.IndicadorIdB = item.IndicadorIdB;

                        variableDetails.AreaIdC = request.AreaIdC;
                        variableDetails.IndicadorIdC = item.IndicadorIdC;

                        variableDetails.LastModifierUserId = currentUserId;
                        variableDetails.LastModificationTime = DateTime.Now;

                        var response = db.SaveChanges();

                        result = true;
                    }
                }
                // End variable B


                //Variable C
                if (request.Variable.Equals(Varibels.C))
                {
                    var variable = db.StrategyActivities.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

                    foreach (var item in request.VariableDetails)
                    {
                        var variableDetails = db.StrategyActivityDetails.Where(x => x.Id == item.Id).FirstOrDefault();

                        variableDetails.AreaIdA = request.AreaIdA;
                        variableDetails.IndicadorIdA = item.IndicadorIdA;

                        variableDetails.AreaIdB = request.AreaIdB;
                        variableDetails.IndicadorIdB = item.IndicadorIdB;

                        variableDetails.AreaIdC = request.AreaIdC;
                        variableDetails.IndicadorIdC = item.IndicadorIdC;

                        variableDetails.LastModifierUserId = currentUserId;
                        variableDetails.LastModificationTime = DateTime.Now;

                        var response = db.SaveChanges();

                        result = true;
                    }
                }
                // End variable C


                //Variable D
                if (request.Variable.Equals(Varibels.D))
                {
                    var variable = db.PedagogicalResources.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

                    foreach (var item in request.VariableDetails)
                    {
                        var variableDetails = db.PedagogicalResourceDetails.Where(x => x.Id == item.Id).FirstOrDefault();

                        variableDetails.AreaIdA = request.AreaIdA;
                        variableDetails.IndicadorIdA = item.IndicadorIdA;

                        variableDetails.AreaIdB = request.AreaIdB;
                        variableDetails.IndicadorIdB = item.IndicadorIdB;

                        variableDetails.AreaIdC = request.AreaIdC;
                        variableDetails.IndicadorIdC = item.IndicadorIdC;

                        variableDetails.LastModifierUserId = currentUserId;
                        variableDetails.LastModificationTime = DateTime.Now;

                        var response = db.SaveChanges();

                        result = true;
                    }
                }
                // End variable D


                //Variable E
                if (request.Variable.Equals(Varibels.E))
                {
                    var variable = db.EvaluationProcesses.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

                    foreach (var item in request.VariableDetails)
                    {
                        var variableDetails = db.EvaluationProcessDetails.Where(x => x.Id == item.Id).FirstOrDefault();

                        variableDetails.AreaIdA = request.AreaIdA;
                        variableDetails.IndicadorIdA = item.IndicadorIdA;

                        variableDetails.AreaIdB = request.AreaIdB;
                        variableDetails.IndicadorIdB = item.IndicadorIdB;

                        variableDetails.AreaIdC = request.AreaIdC;
                        variableDetails.IndicadorIdC = item.IndicadorIdC;

                        variableDetails.LastModifierUserId = currentUserId;
                        variableDetails.LastModificationTime = DateTime.Now;

                        var response = db.SaveChanges();

                        result = true;
                    }
                }
                // End variable E


                //Variable F
                if (request.Variable.Equals(Varibels.F))
                {
                    var variable = db.ClassroomClimates.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

                    foreach (var item in request.VariableDetails)
                    {
                        var variableDetails = db.ClassroomClimateDetails.Where(x => x.Id == item.Id).FirstOrDefault();

                        variableDetails.AreaIdA = request.AreaIdA;
                        variableDetails.IndicadorIdA = item.IndicadorIdA;

                        variableDetails.AreaIdB = request.AreaIdB;
                        variableDetails.IndicadorIdB = item.IndicadorIdB;

                        variableDetails.AreaIdC = request.AreaIdC;
                        variableDetails.IndicadorIdC = item.IndicadorIdC;

                        variableDetails.LastModifierUserId = currentUserId;
                        variableDetails.LastModificationTime = DateTime.Now;

                        var response = db.SaveChanges();

                        result = true;
                    }
                }
                // End variable F


                //Variable G
                if (request.Variable.Equals(Varibels.G))
                {
                    var variable = db.ReflectionPractices.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

                    foreach (var item in request.VariableDetails)
                    {
                        var variableDetails = db.ReflectionPracticeDetails.Where(x => x.Id == item.Id).FirstOrDefault();

                        variableDetails.AreaIdA = request.AreaIdA;
                        variableDetails.IndicadorIdA = item.IndicadorIdA;

                        variableDetails.AreaIdB = request.AreaIdB;
                        variableDetails.IndicadorIdB = item.IndicadorIdB;

                        variableDetails.AreaIdC = request.AreaIdC;
                        variableDetails.IndicadorIdC = item.IndicadorIdC;

                        variableDetails.LastModifierUserId = currentUserId;
                        variableDetails.LastModificationTime = DateTime.Now;

                        var response = db.SaveChanges();

                        result = true;
                    }
                }
                // End variable G


                //Variable H
                if (request.Variable.Equals(Varibels.H))
                {
                    var variable = db.RelationFatherMothers.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

                    foreach (var item in request.VariableDetails)
                    {
                        var variableDetails = db.RelationFatherMotherDetails.Where(x => x.Id == item.Id).FirstOrDefault();

                        variableDetails.AreaIdA = request.AreaIdA;
                        variableDetails.IndicadorIdA = item.IndicadorIdA;

                        variableDetails.AreaIdB = request.AreaIdB;
                        variableDetails.IndicadorIdB = item.IndicadorIdB;

                        variableDetails.AreaIdC = request.AreaIdC;
                        variableDetails.IndicadorIdC = item.IndicadorIdC;

                        variableDetails.LastModifierUserId = currentUserId;
                        variableDetails.LastModificationTime = DateTime.Now;

                        var response = db.SaveChanges();

                        result = true;
                    }
                }
                // End variable H

            }
            catch (Exception ex)
            {

            }

            return result;
        }

    }
}