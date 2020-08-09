using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Request.Domain.RequestFlowAI;
using JS.Base.WS.API.DTO.Response.Domain;
using JS.Base.WS.API.Global;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Models.Domain;
using JS.Base.WS.API.Models.Domain.AccompanyingInstrument;
using JS.Base.WS.API.Models.Domain.RequestFlowAI;
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

        private decimal EfficiencyEvaluateFactor = 0;

        private string indicadorPendingLabel = Indicators.IndicadorPendingLabel;
        private string areaPending = Constants.Areas.Pending;


        public List<AccompInstRequestDto> GetAccompInstRequest()
        {
            var result = new List<AccompInstRequestDto>();

            string viewAllAccompanyingInstrumentRequests_ByRoles = ConfigurationParameter.ViewAllAccompanyingInstrumentRequests_ByRoles;

            var currentUserRole = db.UserRoles.Where(x => x.UserId == currentUserId)
                                               .Select(x => new { roleName = x.Role.ShortName })
                                               .FirstOrDefault();

            if (string.IsNullOrEmpty(viewAllAccompanyingInstrumentRequests_ByRoles))
            {
                viewAllAccompanyingInstrumentRequests_ByRoles = ",";
            }
            var roles = viewAllAccompanyingInstrumentRequests_ByRoles.Split(',');

            if (roles.Count() > 0)
            {
                bool validateRole = false;
                if (currentUserRole != null)
                {
                    validateRole = roles.Contains(currentUserRole.roleName);
                }

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
                                  CanViewResumenOption = rq.RequestStatu.CanViewResumenOption,
                                  EfficiencyGeneralValue = rq.EfficiencyGeneralValue == null ? string.Empty : rq.EfficiencyGeneralValue,
                                  EfficiencyGeneralColour = rq.EfficiencyGeneralColour == null ? string.Empty : rq.EfficiencyGeneralColour,
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
                                  CanViewResumenOption = rq.RequestStatu.CanViewResumenOption,
                                  EfficiencyGeneralValue = rq.EfficiencyGeneralValue == null ? string.Empty : rq.EfficiencyGeneralValue,
                                  EfficiencyGeneralColour = rq.EfficiencyGeneralColour == null ? string.Empty : rq.EfficiencyGeneralColour,
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


            //Comments Revised Document
            #region CommentsRevisedDocument

            var commentsRevisedDocumentDef = db.CommentsRevisedDocumentsDefs.ToList();

            var commentsRevisedDocumentRequest = new CommentsRevisedDocument()
            {
                RequestId = requestId,
                StatusId = inProcessStatus.Id,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var commentsRevisedDocument = db.CommentsRevisedDocuments.Add(commentsRevisedDocumentRequest);
            db.SaveChanges();

            foreach (var item in commentsRevisedDocumentDef)
            {
                var commentsRevisedDocumentsDetailRequest = new CommentsRevisedDocumentsDetail()
                {
                    CommentsRevisedDocumentId = commentsRevisedDocument.Id,
                    CommentsRevisedDocumentsDefId = item.Id,
                    AreaIdA = areaId,
                    CommentA = string.Empty,
                    AreaIdB = areaId,
                    CommentB = string.Empty,
                    AreaIdC = areaId,
                    CommentC = string.Empty,
                    IsActive = true,
                    CreatorUserId = currentUserId,
                    CreationTime = DateTime.Now,
                };

                var commentsRevisedDocumentsDetail = db.CommentsRevisedDocumentsDetails.Add(commentsRevisedDocumentsDetailRequest);
                db.SaveChanges();
            }

            #endregion


            //Description Observation Support Provided
            #region DescriptionObservationSupportProvided

            var descriptionObservationSupportProvidedRequest = new DescriptionObservationSupportProvided()
            {
                RequestId = requestId,
                StatusId = inProcessStatus.Id,

                AreaIdA = areaId,
                CommentA = string.Empty,

                AreaIdB = areaId,
                CommentB = string.Empty,

                AreaIdC = areaId,
                CommentC = string.Empty,

                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var descriptionObservationSupportProvided = db.DescriptionObservationSupportProvideds.Add(descriptionObservationSupportProvidedRequest);
            db.SaveChanges();

            #endregion


            //Suggestions Agreement
            #region SuggestionsAgreement

            var suggestionsAgreementRequest = new SuggestionsAgreement()
            {
                RequestId = requestId,
                StatusId = inProcessStatus.Id,

                AreaIdA = areaId,
                DateA = string.Empty,
                CommentA = string.Empty,
                TeacherSignatureA = string.Empty,
                CompanionSignatureA = string.Empty,
                DistrictTechnicianSignatureA = string.Empty,

                AreaIdB = areaId,
                CommentB = string.Empty,
                DateB = string.Empty,
                TeacherSignatureB = string.Empty,
                CompanionSignatureB = string.Empty,
                DistrictTechnicianSignatureB = string.Empty,

                AreaIdC = areaId,
                DateC = string.Empty,
                CommentC = string.Empty,
                TeacherSignatureC = string.Empty,
                CompanionSignatureC = string.Empty,
                DistrictTechnicianSignatureC = string.Empty,

                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var suggestionsAgreement = db.SuggestionsAgreements.Add(suggestionsAgreementRequest);
            db.SaveChanges();

            #endregion


            //Update Visit A Is Availble
            UpdateVisitAIsAvailble(requestId);


            result = true;

            return result;
        }


        public VariableDto GetVariableByRequestId(long requestId, string variable)
        {
            var result = new VariableDto();
            variable = variable.ToUpper();
            variable = variable.Trim();

            EfficiencyEvaluateFactor = db.Indicators.Where(x => x.IsEvaluationFactor == true).Select(x => x.Value).FirstOrDefault();

            //Request
            var request = db.AccompanyingInstrumentRequests.Where(x => x.Id == requestId).FirstOrDefault();


            //Variable A
            if (variable.Equals(Varibels.A))
            {
                result = db.Plannings.Where(x => x.RequestId == requestId).Select(y => new VariableDto()
                {

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
                    VariableDetails = y.PlanningDetails.Select(p => new VariableDetailsDto()
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


                //Set efficiency
                result = SetEfficiency(result);

                //Update Varible Efficiency
                UpdateVaribleEfficiency(result);

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


                //Set efficiency
                result = SetEfficiency(result);
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

                //Set efficiency
                result = SetEfficiency(result);
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

                //Set efficiency
                result = SetEfficiency(result);
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

                //Set efficiency
                result = SetEfficiency(result);
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

                //Set efficiency
                result = SetEfficiency(result);
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

                //Set efficiency
                result = SetEfficiency(result);
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

                //Set efficiency
                result = SetEfficiency(result);
            }


            //Update Varible Efficiency
            UpdateVaribleEfficiency(result);


            //Calculate Efficiency General
            result.EfficiencyGeneralValue = CalculateGeneralEfficiency(result.RequestId);
            result.EfficiencyGeneralColour = GetColourByEfficiency(Convert.ToDecimal(result.EfficiencyGeneralValue) / 100);
            result.EfficiencyGeneralValue = result.EfficiencyGeneralValue + " %";


            //Set visit available
            result.VisitAIsAvailable = request.VisitAIsAvailable;
            result.VisitBIsAvailable = request.VisitBIsAvailable;
            result.VisitCIsAvailable = request.VisitCIsAvailable;


            //Update request
            UpdateRequest(result.EfficiencyGeneralValue, result.EfficiencyGeneralColour, result.RequestId);


            return result;
        }



        public CommentsRevisedDocumentDto GetCommentsRevisedDocument(long requestId)
        {
            var result = new CommentsRevisedDocumentDto();

            result = db.CommentsRevisedDocuments.Where(x => x.RequestId == requestId).Select(y => new CommentsRevisedDocumentDto()
            {

                Id = y.Id,
                RequestId = y.RequestId,
                StausId = y.StatusId,
                StatusDescription = y.Status.Name,
                StatusColour = y.Status.Colour,
                AreaIdA = y.CommentsRevisedDocumentsDetails.Select(z => z.AreaIdA).FirstOrDefault(),
                DateA = y.CommentsRevisedDocumentsDetails.Select(z => z.DateA).FirstOrDefault(),
                AreaIdB = y.CommentsRevisedDocumentsDetails.Select(z => z.AreaIdB).FirstOrDefault(),
                DateB = y.CommentsRevisedDocumentsDetails.Select(z => z.DateB).FirstOrDefault(),
                AreaIdC = y.CommentsRevisedDocumentsDetails.Select(z => z.AreaIdC).FirstOrDefault(),
                DateC = y.CommentsRevisedDocumentsDetails.Select(z => z.DateC).FirstOrDefault(),
                CommentsRevisedDocumenDetails = y.CommentsRevisedDocumentsDetails.Select(p => new Detail()
                {
                    Id = p.Id,
                    Description = p.CommentsRevisedDocumentsDef.Description,
                    AreaIdA = p.AreaIdA,
                    DateA = p.DateA,
                    CommentA = p.CommentA,
                    AreaIdB = p.AreaIdB,
                    DateB = p.DateB,
                    CommentB = p.CommentB,
                    AreaIdC = p.AreaIdC,
                    DateC = p.DateC,
                    CommentC = p.CommentC,
                }).ToList(),

            }).FirstOrDefault();

            return result;
        }


        public DescriptionObservationSupportProvidedDto GetDescriptionObservationSupportProvided(long requestId)
        {
            var result = new DescriptionObservationSupportProvidedDto();

            result = db.DescriptionObservationSupportProvideds.Where(x => x.RequestId == requestId).Select(y => new DescriptionObservationSupportProvidedDto()
            {

                Id = y.Id,
                RequestId = y.RequestId,
                StausId = y.StatusId,
                StatusDescription = y.Status.Name,
                StatusColour = y.Status.Colour,

                AreaIdA = y.AreaIdA,
                DateA = y.DateA,
                CommentA = y.CommentA,

                AreaIdB = y.AreaIdB,
                DateB = y.DateB,
                CommentB = y.CommentB,

                AreaIdC = y.AreaIdC,
                DateC = y.DateC,
                CommentC = y.CommentC,

            }).FirstOrDefault();

            return result;
        }


        public SuggestionsAgreementDto GetSuggestionsAgreement(long requestId)
        {
            var result = new SuggestionsAgreementDto();

            result = db.SuggestionsAgreements.Where(x => x.RequestId == requestId).Select(y => new SuggestionsAgreementDto()
            {

                Id = y.Id,
                RequestId = y.RequestId,
                StausId = y.StatusId,
                StatusDescription = y.Status.Name,
                StatusColour = y.Status.Colour,

                AreaIdA = y.AreaIdA,
                DateA = y.DateA,
                CommentA = y.CommentA,
                TeacherSignatureA = y.TeacherSignatureA,
                CompanionSignatureA = y.CompanionSignatureA,
                DistrictTechnicianSignatureA = y.DistrictTechnicianSignatureA,

                AreaIdB = y.AreaIdB,
                DateB = y.DateB,
                CommentB = y.CommentB,
                TeacherSignatureB = y.TeacherSignatureB,
                CompanionSignatureB = y.CompanionSignatureB,
                DistrictTechnicianSignatureB = y.DistrictTechnicianSignatureB,

                AreaIdC = y.AreaIdC,
                DateC = y.DateC,
                CommentC = y.CommentC,
                TeacherSignatureC = y.TeacherSignatureC,
                CompanionSignatureC = y.CompanionSignatureC,
                DistrictTechnicianSignatureC = y.DistrictTechnicianSignatureC,

            }).FirstOrDefault();

            return result;
        }



        public VariableDto UpdateVariable(VariableDto request)
        {
            var result = new VariableDto();
            result = request;

            result.Error = false;
            result.ErrorMessage = string.Empty;

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
                    }


                    //Update Visit B Is Availble
                    var areaA = db.Areas.Where(x => x.Id == request.AreaIdA).FirstOrDefault();

                    if (!areaA.ShortName.Equals(areaPending))
                    {
                        UpdateVisitBIsAvailble(request.RequestId);
                    }

                    //Update Visit C Is Availble
                    var areaB = db.Areas.Where(x => x.Id == request.AreaIdB).FirstOrDefault();

                    if (!areaB.ShortName.Equals(areaPending))
                    {
                        UpdateVisitCIsAvailble(request.RequestId);
                    }

                }
                // End variable H

            }
            catch (Exception ex)
            {

            }

            return result;
        }


        public bool UpdateCommentsRevisedDocument(CommentsRevisedDocumentDto request)
        {
            bool result = false;

            var commentsRevisedDocument = db.CommentsRevisedDocuments.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

            foreach (var item in request.CommentsRevisedDocumenDetails)
            {
                var commentsRevisedDocumenDetails = db.CommentsRevisedDocumentsDetails.Where(x => x.Id == item.Id).FirstOrDefault();

                commentsRevisedDocumenDetails.AreaIdA = request.AreaIdA;
                commentsRevisedDocumenDetails.DateA = request.DateA;
                commentsRevisedDocumenDetails.CommentA = item.CommentA;

                commentsRevisedDocumenDetails.AreaIdB = request.AreaIdB;
                commentsRevisedDocumenDetails.DateB = request.DateB;
                commentsRevisedDocumenDetails.CommentB = item.CommentB;

                commentsRevisedDocumenDetails.AreaIdC = request.AreaIdC;
                commentsRevisedDocumenDetails.DateC = request.DateC;
                commentsRevisedDocumenDetails.CommentC = item.CommentC;

                commentsRevisedDocumenDetails.LastModifierUserId = currentUserId;
                commentsRevisedDocumenDetails.LastModificationTime = DateTime.Now;

                var response = db.SaveChanges();

                result = true;
            }

            return result;
        }


        public bool UpdateDescriptionObservationSupportProvided(DescriptionObservationSupportProvidedDto request)
        {
            bool result = false;

            var descriptionObs = db.DescriptionObservationSupportProvideds.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

            descriptionObs.AreaIdA = request.AreaIdA;
            descriptionObs.DateA = request.DateA;
            descriptionObs.CommentA = request.CommentA;

            descriptionObs.AreaIdB = request.AreaIdB;
            descriptionObs.DateB = request.DateB;
            descriptionObs.CommentB = request.CommentB;

            descriptionObs.AreaIdC = request.AreaIdC;
            descriptionObs.DateC = request.DateC;
            descriptionObs.CommentC = request.CommentC;

            descriptionObs.LastModifierUserId = currentUserId;
            descriptionObs.LastModificationTime = DateTime.Now;

            db.SaveChanges();
            result = true;

            return result;
        }


        public bool UpdateSuggestionsAgreement(SuggestionsAgreementDto request)
        {
            bool result = false;

            var suggestionsAgreement = db.SuggestionsAgreements.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

            suggestionsAgreement.AreaIdA = request.AreaIdA;
            suggestionsAgreement.DateA = request.DateA;
            suggestionsAgreement.CommentA = request.CommentA;
            suggestionsAgreement.TeacherSignatureA = request.TeacherSignatureA;
            suggestionsAgreement.CompanionSignatureA = request.CompanionSignatureA;
            suggestionsAgreement.DistrictTechnicianSignatureA = request.DistrictTechnicianSignatureA;

            suggestionsAgreement.AreaIdB = request.AreaIdB;
            suggestionsAgreement.DateB = request.DateB;
            suggestionsAgreement.CommentB = request.CommentB;
            suggestionsAgreement.TeacherSignatureB = request.TeacherSignatureB;
            suggestionsAgreement.CompanionSignatureB = request.CompanionSignatureB;
            suggestionsAgreement.DistrictTechnicianSignatureB = request.DistrictTechnicianSignatureB;

            suggestionsAgreement.AreaIdC = request.AreaIdC;
            suggestionsAgreement.DateC = request.DateC;
            suggestionsAgreement.CommentC = request.CommentC;
            suggestionsAgreement.TeacherSignatureC = request.TeacherSignatureC;
            suggestionsAgreement.CompanionSignatureC = request.CompanionSignatureC;
            suggestionsAgreement.DistrictTechnicianSignatureC = request.DistrictTechnicianSignatureC;

            suggestionsAgreement.LastModifierUserId = currentUserId;
            suggestionsAgreement.LastModificationTime = DateTime.Now;

            db.SaveChanges();
            result = true;

            return result;
        }


        public bool CompleteRequest(long requestId)
        {
            bool result = false;

            int statusCompletedeId = db.RequestStatus.Where(x => x.ShortName == Constants.RequestStatus.Completed).Select(y => y.Id).FirstOrDefault();
            int statusPendingToApproveId = db.RequestStatus.Where(x => x.ShortName == Constants.RequestStatus.PendingToApprove).Select(y => y.Id).FirstOrDefault();

            var requestFlowRequest = new RequestFlowAICompleted()
            {
                RequestId = requestId,
                StatusId = statusCompletedeId,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var requestFlow = db.RequestFlowAICompleteds.Add(requestFlowRequest);

            var request = db.AccompanyingInstrumentRequests.Where(x => x.Id == requestId).FirstOrDefault();
            request.StatusId = statusPendingToApproveId;
            db.SaveChanges();

            result = true;

            return result;
        }


        public bool ApproveRequest(long requestId)
        {
            bool result = false;

            int statusApproveId = db.RequestStatus.Where(x => x.ShortName == Constants.RequestStatus.Approved).Select(y => y.Id).FirstOrDefault();

            var requestFlowRequest = new RequestFlowAIApproved()
            {
                RequestId = requestId,
                StatusId = statusApproveId,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var requestFlow = db.RequestFlowAIApproveds.Add(requestFlowRequest);

            var request = db.AccompanyingInstrumentRequests.Where(x => x.Id == requestId).FirstOrDefault();
            request.StatusId = statusApproveId;
            request.ClosingDate = DateTime.Now;
            db.SaveChanges();

            result = true;

            return result;
        }


        public bool SendToObservationRequest(long requestId)
        {
            bool result = false;

            int statusInObservationId = db.RequestStatus.Where(x => x.ShortName == Constants.RequestStatus.InObservation).Select(y => y.Id).FirstOrDefault();

            var requestFlowRequest = new RequestFlowAIInObservation()
            {
                RequestId = requestId,
                StatusId = statusInObservationId,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var requestFlow = db.RequestFlowAIInObservations.Add(requestFlowRequest);

            var request = db.AccompanyingInstrumentRequests.Where(x => x.Id == requestId).FirstOrDefault();
            request.StatusId = statusInObservationId;
            db.SaveChanges();

            result = true;

            return result;
        }


        public bool Process(long requestId)
        {
            bool result = false;

            int statusInProcessId = db.RequestStatus.Where(x => x.ShortName == Constants.RequestStatus.InProcess).Select(y => y.Id).FirstOrDefault();

            var requestFlowRequest = new RequestFlowAIInProcess()
            {
                RequestId = requestId,
                StatusId = statusInProcessId,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var requestFlow = db.RequestFlowAIInProcesses.Add(requestFlowRequest);

            var request = db.AccompanyingInstrumentRequests.Where(x => x.Id == requestId).FirstOrDefault();
            request.StatusId = statusInProcessId;
            db.SaveChanges();

            result = true;

            return result;
        }


        public bool CancelRequest(long requestId)
        {
            bool result = false;

            int statusCancelId = db.RequestStatus.Where(x => x.ShortName == Constants.RequestStatus.Cancelad).Select(y => y.Id).FirstOrDefault();

            var requestFlowRequest = new RequestFlowAICancelad()
            {
                RequestId = requestId,
                StatusId = statusCancelId,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var requestFlow = db.RequestFlowAICancelads.Add(requestFlowRequest);

            var request = db.AccompanyingInstrumentRequests.Where(x => x.Id == requestId).FirstOrDefault();
            request.StatusId = statusCancelId;
            db.SaveChanges();

            result = true;

            return result;
        }


        public bool CreateResearchSummary(ResearchSummaryDto request)
        {
            bool response = false;

            var researchSummaryRequest = new ResearchSummary()
            {
                RequestId = request.RequestId,
                Summary = request.Summary,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var result = db.ResearchSummaries.Add(researchSummaryRequest);
            db.SaveChanges();

            response = true;

            return response;
        }



        #region Private method

        // Calculate Efficiency
        private CalculateEfficiency CalculateEfficiency(VariableDto request)
        {
            var response = new CalculateEfficiency();

            if (request == null)
            {
                response.Error = true;
                response.ErrorMessage = "Solicitud inválida";
            }

            decimal _efficiencyValueA = 0;
            decimal _efficiencyValueB = 0;
            decimal _efficiencyValueC = 0;
            decimal _efficiencyTotalValue = 0;

            int visitQuantity = 0;

            if (request == null)
            {
                response.Error = true;
                response.ErrorMessage = "Solicitud inválida";

                return response;
            }

            var areaA = db.Areas.Where(x => x.Id == request.AreaIdA).FirstOrDefault();
            var areaB = db.Areas.Where(x => x.Id == request.AreaIdB).FirstOrDefault();
            var areaC = db.Areas.Where(x => x.Id == request.AreaIdC).FirstOrDefault();

            if (!areaA.ShortName.Equals(areaPending))
            {
                visitQuantity += 1;
            }
            if (!areaB.ShortName.Equals(areaPending))
            {
                visitQuantity += 1;
            }
            if (!areaC.ShortName.Equals(areaPending))
            {
                visitQuantity += 1;
            }

            foreach (var item in request.VariableDetails)
            {
                var currentIndicatorA = db.Indicators.Where(x => x.Id == item.IndicadorIdA).FirstOrDefault();
                var currentIndicatorB = db.Indicators.Where(x => x.Id == item.IndicadorIdB).FirstOrDefault();
                var currentIndicatorC = db.Indicators.Where(x => x.Id == item.IndicadorIdC).FirstOrDefault();

                _efficiencyValueA += currentIndicatorA.Value;
                _efficiencyValueB += currentIndicatorB.Value;
                _efficiencyValueC += currentIndicatorC.Value;
            }

            _efficiencyValueA = _efficiencyValueA / (EfficiencyEvaluateFactor * (decimal)request.VariableDetails.Count());
            _efficiencyValueB = _efficiencyValueB / (EfficiencyEvaluateFactor * (decimal)request.VariableDetails.Count());
            _efficiencyValueC = _efficiencyValueC / (EfficiencyEvaluateFactor * (decimal)request.VariableDetails.Count());


            //Validate indicators
            if (areaA.ShortName.Equals(areaPending) && _efficiencyValueA > 0)
            {
                response.Error = true;
                response.ErrorMessage = "No puedes marcar un indicador, teniendo el área pendiente de la primera visita";

                return response;
            }
            if (areaB.ShortName.Equals(areaPending) && _efficiencyValueB > 0)
            {
                response.Error = true;
                response.ErrorMessage = "No puedes marcar un indicador, teniendo el área pendiente de la segunda visita";

                return response;
            }
            if (areaC.ShortName.Equals(areaPending) && _efficiencyValueC > 0)
            {
                response.Error = true;
                response.ErrorMessage = "No puedes marcar un indicador, teniendo el área pendiente de la tercera visita";

                return response;
            }

            if (visitQuantity > 0)
            {
                _efficiencyTotalValue = (_efficiencyValueA + _efficiencyValueB + _efficiencyValueC) / visitQuantity;
            }

            response.EfficiencyValueA = _efficiencyValueA;
            response.EfficiencyValueB = _efficiencyValueB;
            response.EfficiencyValueC = _efficiencyValueC;
            response.EfficiencyTotalValue = _efficiencyTotalValue;


            return response;
        }


        //Set Efficiency
        private VariableDto SetEfficiency(VariableDto request)
        {
            if (request == null)
            {
                var _request = new VariableDto();
                _request.Error = true;
                _request.ErrorMessage = "Solicitud inválida";

                return request;
            }

            var efficiency = CalculateEfficiency(request);

            request.EfficiencyValueA = Math.Ceiling(efficiency.EfficiencyValueA * 100).ToString() + " %";
            request.EfficiencyColourA = GetColourByEfficiency(efficiency.EfficiencyValueA);

            request.EfficiencyValueB = Math.Ceiling(efficiency.EfficiencyValueB * 100).ToString() + " %";
            request.EfficiencyColourB = GetColourByEfficiency(efficiency.EfficiencyValueB);

            request.EfficiencyValueC = Math.Ceiling(efficiency.EfficiencyValueC * 100).ToString() + " %";
            request.EfficiencyColourC = GetColourByEfficiency(efficiency.EfficiencyValueC);

            request.EfficiencyTotalValue = Math.Ceiling(efficiency.EfficiencyTotalValue * 100).ToString() + " %";
            request.EfficiencyTotalColour = GetColourByEfficiency(efficiency.EfficiencyTotalValue);

            request.EfficiencyEvaluateFactor = ((int)EfficiencyEvaluateFactor).ToString();

            request.Error = efficiency.Error;
            request.ErrorMessage = efficiency.ErrorMessage;


            //Validate visit
            var areaA = db.Areas.Where(x => x.Id == request.AreaIdA).FirstOrDefault();
            var areaB = db.Areas.Where(x => x.Id == request.AreaIdB).FirstOrDefault();
            var areaC = db.Areas.Where(x => x.Id == request.AreaIdC).FirstOrDefault();
            int quantityAreaPending = 0;

            if (areaA.ShortName.Equals(areaPending))
            {
                request.EfficiencyValueA = indicadorPendingLabel;
                request.EfficiencyColourA = "btn btn-default";
                quantityAreaPending += 1;
            }
            if (areaB.ShortName.Equals(areaPending))
            {
                request.EfficiencyValueB = indicadorPendingLabel;
                request.EfficiencyColourB = "btn btn-default";
                quantityAreaPending += 1;
            }
            if (areaC.ShortName.Equals(areaPending))
            {
                request.EfficiencyValueC = indicadorPendingLabel;
                request.EfficiencyColourC = "btn btn-default";
                quantityAreaPending += 1;
            }

            if (quantityAreaPending == 3)
            {
                request.EfficiencyTotalValue = indicadorPendingLabel;
                request.EfficiencyTotalColour = "btn btn-default";
            }

            return request;
        }


        //Get Colour By Efficiency
        private string GetColourByEfficiency(decimal value)
        {
            string result = string.Empty;

            value = Math.Ceiling(value * 100);

            if (value >= 90)
            {
                result = "btn btn-success";
            }
            if (value >= 80 && value < 90)
            {
                result = "btn btn-primary";
            }
            if (value >= 70 && value < 80)
            {
                result = "btn btn-info";
            }
            if (value >= 60 && value < 70)
            {
                result = "btn btn-warning";
            }
            if (value <= 60)
            {
                result = "btn btn-danger";
            }

            return result;
        }



        //Update Varible Efficiency
        private void UpdateVaribleEfficiency(VariableDto request)
        {
            //Variable A
            if (request.Variable.Equals(Varibels.A))
            {
                var variable = db.Plannings.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

                variable.EfficiencyValueA = request.EfficiencyValueA;
                variable.EfficiencyColourA = request.EfficiencyColourA;

                variable.EfficiencyValueB = request.EfficiencyValueB;
                variable.EfficiencyColourB = request.EfficiencyColourB;

                variable.EfficiencyValueC = request.EfficiencyValueC;
                variable.EfficiencyColourC = request.EfficiencyColourC;

                variable.EfficiencyTotalValue = request.EfficiencyTotalValue;
                variable.EfficiencyTotalColour = request.EfficiencyTotalColour;

                variable.EfficiencyEvaluateFactor = request.EfficiencyEvaluateFactor;

                db.SaveChanges();
            }


            //Variable B
            if (request.Variable.Equals(Varibels.B))
            {
                var variable = db.ContentDomains.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

                variable.EfficiencyValueA = request.EfficiencyValueA;
                variable.EfficiencyColourA = request.EfficiencyColourA;

                variable.EfficiencyValueB = request.EfficiencyValueB;
                variable.EfficiencyColourB = request.EfficiencyColourB;

                variable.EfficiencyValueC = request.EfficiencyValueC;
                variable.EfficiencyColourC = request.EfficiencyColourC;

                variable.EfficiencyTotalValue = request.EfficiencyTotalValue;
                variable.EfficiencyTotalColour = request.EfficiencyTotalColour;

                variable.EfficiencyEvaluateFactor = request.EfficiencyEvaluateFactor;

                db.SaveChanges();
            }


            //Variable C
            if (request.Variable.Equals(Varibels.C))
            {
                var variable = db.StrategyActivities.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

                variable.EfficiencyValueA = request.EfficiencyValueA;
                variable.EfficiencyColourA = request.EfficiencyColourA;

                variable.EfficiencyValueB = request.EfficiencyValueB;
                variable.EfficiencyColourB = request.EfficiencyColourB;

                variable.EfficiencyValueC = request.EfficiencyValueC;
                variable.EfficiencyColourC = request.EfficiencyColourC;

                variable.EfficiencyTotalValue = request.EfficiencyTotalValue;
                variable.EfficiencyTotalColour = request.EfficiencyTotalColour;

                variable.EfficiencyEvaluateFactor = request.EfficiencyEvaluateFactor;

                db.SaveChanges();
            }


            //Variable D
            if (request.Variable.Equals(Varibels.D))
            {
                var variable = db.PedagogicalResources.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

                variable.EfficiencyValueA = request.EfficiencyValueA;
                variable.EfficiencyColourA = request.EfficiencyColourA;

                variable.EfficiencyValueB = request.EfficiencyValueB;
                variable.EfficiencyColourB = request.EfficiencyColourB;

                variable.EfficiencyValueC = request.EfficiencyValueC;
                variable.EfficiencyColourC = request.EfficiencyColourC;

                variable.EfficiencyTotalValue = request.EfficiencyTotalValue;
                variable.EfficiencyTotalColour = request.EfficiencyTotalColour;

                variable.EfficiencyEvaluateFactor = request.EfficiencyEvaluateFactor;

                db.SaveChanges();
            }


            //Variable E
            if (request.Variable.Equals(Varibels.E))
            {
                var variable = db.EvaluationProcesses.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

                variable.EfficiencyValueA = request.EfficiencyValueA;
                variable.EfficiencyColourA = request.EfficiencyColourA;

                variable.EfficiencyValueB = request.EfficiencyValueB;
                variable.EfficiencyColourB = request.EfficiencyColourB;

                variable.EfficiencyValueC = request.EfficiencyValueC;
                variable.EfficiencyColourC = request.EfficiencyColourC;

                variable.EfficiencyTotalValue = request.EfficiencyTotalValue;
                variable.EfficiencyTotalColour = request.EfficiencyTotalColour;

                variable.EfficiencyEvaluateFactor = request.EfficiencyEvaluateFactor;

                db.SaveChanges();
            }


            //Variable F
            if (request.Variable.Equals(Varibels.F))
            {
                var variable = db.ClassroomClimates.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

                variable.EfficiencyValueA = request.EfficiencyValueA;
                variable.EfficiencyColourA = request.EfficiencyColourA;

                variable.EfficiencyValueB = request.EfficiencyValueB;
                variable.EfficiencyColourB = request.EfficiencyColourB;

                variable.EfficiencyValueC = request.EfficiencyValueC;
                variable.EfficiencyColourC = request.EfficiencyColourC;

                variable.EfficiencyTotalValue = request.EfficiencyTotalValue;
                variable.EfficiencyTotalColour = request.EfficiencyTotalColour;

                variable.EfficiencyEvaluateFactor = request.EfficiencyEvaluateFactor;

                db.SaveChanges();
            }


            //Variable G
            if (request.Variable.Equals(Varibels.G))
            {
                var variable = db.ReflectionPractices.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

                variable.EfficiencyValueA = request.EfficiencyValueA;
                variable.EfficiencyColourA = request.EfficiencyColourA;

                variable.EfficiencyValueB = request.EfficiencyValueB;
                variable.EfficiencyColourB = request.EfficiencyColourB;

                variable.EfficiencyValueC = request.EfficiencyValueC;
                variable.EfficiencyColourC = request.EfficiencyColourC;

                variable.EfficiencyTotalValue = request.EfficiencyTotalValue;
                variable.EfficiencyTotalColour = request.EfficiencyTotalColour;

                variable.EfficiencyEvaluateFactor = request.EfficiencyEvaluateFactor;

                db.SaveChanges();
            }


            //Variable H
            if (request.Variable.Equals(Varibels.H))
            {
                var variable = db.RelationFatherMothers.Where(x => x.RequestId == request.RequestId).FirstOrDefault();

                variable.EfficiencyValueA = request.EfficiencyValueA;
                variable.EfficiencyColourA = request.EfficiencyColourA;

                variable.EfficiencyValueB = request.EfficiencyValueB;
                variable.EfficiencyColourB = request.EfficiencyColourB;

                variable.EfficiencyValueC = request.EfficiencyValueC;
                variable.EfficiencyColourC = request.EfficiencyColourC;

                variable.EfficiencyTotalValue = request.EfficiencyTotalValue;
                variable.EfficiencyTotalColour = request.EfficiencyTotalColour;

                variable.EfficiencyEvaluateFactor = request.EfficiencyEvaluateFactor;

                db.SaveChanges();
            }

        }


        //Validate Update Variable
        private ValidateUpdateVariable ValidateUpdateVariable(VariableDto request)
        {
            var result = new ValidateUpdateVariable();
            result.Variable = request;

            decimal indicatorValueA = 0;
            decimal indicatorValueB = 0;
            decimal indicatorValueC = 0;

            var areaA = db.Areas.Where(x => x.Id == request.AreaIdA).FirstOrDefault();
            var areaB = db.Areas.Where(x => x.Id == request.AreaIdB).FirstOrDefault();
            var areaC = db.Areas.Where(x => x.Id == request.AreaIdC).FirstOrDefault();

            foreach (var item in request.VariableDetails)
            {
                var currentIndicatorA = db.Indicators.Where(x => x.Id == item.IndicadorIdA).FirstOrDefault();
                var currentIndicatorB = db.Indicators.Where(x => x.Id == item.IndicadorIdB).FirstOrDefault();
                var currentIndicatorC = db.Indicators.Where(x => x.Id == item.IndicadorIdC).FirstOrDefault();

                indicatorValueA += currentIndicatorA.Value;
                indicatorValueB += currentIndicatorA.Value;
                indicatorValueC += currentIndicatorC.Value;
            }

            if (areaA.ShortName.Equals(areaPending) && indicatorValueA > 0)
            {
                result.Error = true;
                result.ErrorMessage = "No puedes marcar un indicador, teniendo el área pendiente de la primera visita";
            }
            if (areaB.ShortName.Equals(areaPending) && indicatorValueB > 0)
            {
                result.Error = true;
                result.ErrorMessage = "No puedes marcar un indicador, teniendo el área pendiente de la segunda visita";
            }
            if (areaC.ShortName.Equals(areaPending) && indicatorValueC > 0)
            {
                result.Error = true;
                result.ErrorMessage = "No puedes marcar un indicador, teniendo el área pendiente de la tercera visita";
            }

            return result;
        }


        //Calculate General Efficiency
        private string CalculateGeneralEfficiency(long requestId)
        {
            string result = string.Empty;

            int quantityVariable = 0;
            decimal totalValue = 0;

            var variableA = db.Plannings.Where(x => x.RequestId == requestId).FirstOrDefault();
            var variableB = db.ContentDomains.Where(x => x.RequestId == requestId).FirstOrDefault();
            var variableC = db.StrategyActivities.Where(x => x.RequestId == requestId).FirstOrDefault();
            var variableD = db.PedagogicalResources.Where(x => x.RequestId == requestId).FirstOrDefault();
            var variableE = db.EvaluationProcesses.Where(x => x.RequestId == requestId).FirstOrDefault();
            var variableF = db.ClassroomClimates.Where(x => x.RequestId == requestId).FirstOrDefault();
            var variableG = db.ReflectionPractices.Where(x => x.RequestId == requestId).FirstOrDefault();
            var variableH = db.RelationFatherMothers.Where(x => x.RequestId == requestId).FirstOrDefault();

            //Variable A
            if (variableA.EfficiencyTotalValue != null)
            {
                if (!variableA.EfficiencyTotalValue.Equals(indicadorPendingLabel))
                {
                    quantityVariable += 1;
                    variableA.EfficiencyTotalValue = variableA.EfficiencyTotalValue.Replace(" %", "");

                    totalValue += Convert.ToDecimal(variableA.EfficiencyTotalValue);
                }

            }


            //Variable B
            if (variableB.EfficiencyTotalValue != null)
            {
                if (!variableB.EfficiencyTotalValue.Equals(indicadorPendingLabel))
                {
                    quantityVariable += 1;
                    variableB.EfficiencyTotalValue = variableB.EfficiencyTotalValue.Replace(" %", "");

                    totalValue += Convert.ToDecimal(variableB.EfficiencyTotalValue);
                }
            }


            //Variable C
            if (variableC.EfficiencyTotalValue != null)
            {
                if (!variableC.EfficiencyTotalValue.Equals(indicadorPendingLabel))
                {
                    quantityVariable += 1;
                    variableC.EfficiencyTotalValue = variableC.EfficiencyTotalValue.Replace(" %", "");

                    totalValue += Convert.ToDecimal(variableC.EfficiencyTotalValue);
                }
            }


            //Variable D
            if (variableD.EfficiencyTotalValue != null)
            {
                if (!variableD.EfficiencyTotalValue.Equals(indicadorPendingLabel))
                {
                    quantityVariable += 1;
                    variableD.EfficiencyTotalValue = variableD.EfficiencyTotalValue.Replace(" %", "");

                    totalValue += Convert.ToDecimal(variableD.EfficiencyTotalValue);
                }
            }


            //Variable E
            if (variableE.EfficiencyTotalValue != null)
            {
                if (!variableE.EfficiencyTotalValue.Equals(indicadorPendingLabel))
                {
                    quantityVariable += 1;
                    variableE.EfficiencyTotalValue = variableE.EfficiencyTotalValue.Replace(" %", "");

                    totalValue += Convert.ToDecimal(variableE.EfficiencyTotalValue);
                }
            }


            //Variable F
            if (variableF.EfficiencyTotalValue != null)
            {
                if (!variableF.EfficiencyTotalValue.Equals(indicadorPendingLabel))
                {
                    quantityVariable += 1;
                    variableF.EfficiencyTotalValue = variableF.EfficiencyTotalValue.Replace(" %", "");

                    totalValue += Convert.ToDecimal(variableF.EfficiencyTotalValue);
                }
            }


            //Variable G
            if (variableG.EfficiencyTotalValue != null)
            {
                if (!variableG.EfficiencyTotalValue.Equals(indicadorPendingLabel))
                {
                    quantityVariable += 1;
                    variableG.EfficiencyTotalValue = variableG.EfficiencyTotalValue.Replace(" %", "");

                    totalValue += Convert.ToDecimal(variableG.EfficiencyTotalValue);
                }
            }


            //Variable H
            if (variableH.EfficiencyTotalValue != null)
            {
                if (!variableH.EfficiencyTotalValue.Equals(indicadorPendingLabel))
                {
                    quantityVariable += 1;
                    variableH.EfficiencyTotalValue = variableH.EfficiencyTotalValue.Replace(" %", "");

                    totalValue += Convert.ToDecimal(variableH.EfficiencyTotalValue);
                }
            }


            if (quantityVariable > 0)
            {
                totalValue = totalValue / quantityVariable;
            }

            result = Math.Ceiling(totalValue).ToString();
            return result;
        }


        //Update request
        private void UpdateRequest(string efficiencyGeneralValue, string efficiencyGeneralColour, long requestId)
        {
            var variable = db.AccompanyingInstrumentRequests.Where(x => x.Id == requestId).FirstOrDefault();

            variable.EfficiencyGeneralValue = efficiencyGeneralValue;
            variable.EfficiencyGeneralColour = efficiencyGeneralColour;

            db.SaveChanges();
        }


        //The update visit A is available
        private void UpdateVisitAIsAvailble(long requestId)
        {
            var request = db.AccompanyingInstrumentRequests.Where(x => x.Id == requestId).FirstOrDefault();

            request.VisitAIsAvailable = true;
            db.SaveChanges();
        }

        //The update visit B is available
        private void UpdateVisitBIsAvailble(long requestId)
        {
            var request = db.AccompanyingInstrumentRequests.Where(x => x.Id == requestId).FirstOrDefault();

            request.VisitBIsAvailable = true;
            db.SaveChanges();
        }

        //The update visit C is available
        private void UpdateVisitCIsAvailble(long requestId)
        {
            var request = db.AccompanyingInstrumentRequests.Where(x => x.Id == requestId).FirstOrDefault();

            request.VisitCIsAvailable = true;
            db.SaveChanges();
        }


        #endregion
    }




    #region Private Class

    public class CalculateEfficiency
    {
        public decimal EfficiencyValueA { get; set; }
        public decimal EfficiencyValueB { get; set; }
        public decimal EfficiencyValueC { get; set; }
        public decimal EfficiencyTotalValue { get; set; }

        //Error
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ValidateUpdateVariable
    {
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
        public VariableDto Variable { get; set; }
    }

    #endregion

}