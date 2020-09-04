using JS.Base.WS.API.Models;
using JS.Base.WS.API.Models.Authorization;
using JS.Base.WS.API.Models.Configuration;
using JS.Base.WS.API.Models.Domain;
using JS.Base.WS.API.Models.Domain.AccompanyingInstrument;
using JS.Base.WS.API.Models.Domain.RequestFlowAI;
using JS.Base.WS.API.Models.FileDocument;
using JS.Base.WS.API.Models.Permission;
using JS.Base.WS.API.Models.PersonProfile;
using JS.Base.WS.API.Models.Publicity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DBContext
{
    public class MyDBcontext: DbContext
    {

        public MyDBcontext() : base("name=JS.Base")
        {

        }

        //metodo para eliminar la plurarizacion de las entidades
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //}

        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<LocatorType> LocatorTypes { get; set; }
        public virtual DbSet<Locator> Locators { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserStatus> UserStatus { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<EntityAction> EntityActions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<SystemConfiguration> SystemConfigurations { get; set; }
        public virtual DbSet<ConfigurationParameter> ConfigurationParameters { get; set; }
        public virtual DbSet<PersonType> PersonTypes { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }


        //Publicity
        public virtual DbSet<Template> Templates { get; set; }

        //File
        public virtual DbSet<FileDocument> FileDocuments  { get; set; }



        //Domin
        public virtual DbSet<Regional> Regionals { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<EducativeCenter> EducativeCenters { get; set; }
        public virtual DbSet<Tanda> Tandas { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Docent> Docents { get; set; }
        public virtual DbSet<RequestStatus> RequestStatus { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<Indicator> Indicators { get; set; }
        public virtual DbSet<RequestFlowAICompleted> RequestFlowAICompleteds { get; set; }
        public virtual DbSet<RequestFlowAIApproved> RequestFlowAIApproveds { get; set; }
        public virtual DbSet<RequestFlowAICancelad> RequestFlowAICancelads { get; set; }
        public virtual DbSet<RequestFlowAIInObservation> RequestFlowAIInObservations { get; set; }
        public virtual DbSet<RequestFlowAIInProcess> RequestFlowAIInProcesses { get; set; }
        public virtual DbSet<ResearchSummary> ResearchSummaries { get; set; }


        //Accompanying InstrumentRequest
        public virtual DbSet<AccompanyingInstrumentRequest> AccompanyingInstrumentRequests { get; set; }
        public virtual DbSet<IdentificationData> IdentificationDatas { get; set; }
        public virtual DbSet<Variable> Variables { get; set; }
        public virtual DbSet<VariableDetail> VariableDetails { get; set; }
        public virtual DbSet<Planning> Plannings { get; set; }
        public virtual DbSet<PlanningDetail> PlanningDetails { get; set; }
        public virtual DbSet<ContentDomain> ContentDomains { get; set; }
        public virtual DbSet<ContentDomainDetail> ContentDomainDetails { get; set; }
        public virtual DbSet<StrategyActivity> StrategyActivities { get; set; }
        public virtual DbSet<StrategyActivityDetail> StrategyActivityDetails { get; set; }
        public virtual DbSet<PedagogicalResource> PedagogicalResources { get; set; }
        public virtual DbSet<PedagogicalResourceDetail> PedagogicalResourceDetails { get; set; }
        public virtual DbSet<EvaluationProcess> EvaluationProcesses { get; set; }
        public virtual DbSet<EvaluationProcessDetail> EvaluationProcessDetails { get; set; }
        public virtual DbSet<ClassroomClimate> ClassroomClimates { get; set; }
        public virtual DbSet<ClassroomClimateDetail> ClassroomClimateDetails { get; set; }
        public virtual DbSet<ReflectionPractice> ReflectionPractices { get; set; }
        public virtual DbSet<ReflectionPracticeDetail> ReflectionPracticeDetails { get; set; }
        public virtual DbSet<RelationFatherMother> RelationFatherMothers { get; set; }
        public virtual DbSet<RelationFatherMotherDetail> RelationFatherMotherDetails { get; set; }
        public virtual DbSet<CommentsRevisedDocumentsDef> CommentsRevisedDocumentsDefs { get; set; }
        public virtual DbSet<CommentsRevisedDocument> CommentsRevisedDocuments { get; set; }
        public virtual DbSet<CommentsRevisedDocumentsDetail> CommentsRevisedDocumentsDetails { get; set; }
        public virtual DbSet<DescriptionObservationSupportProvided> DescriptionObservationSupportProvideds { get; set; }
        public virtual DbSet<SuggestionsAgreement> SuggestionsAgreements { get; set; }


    }
}