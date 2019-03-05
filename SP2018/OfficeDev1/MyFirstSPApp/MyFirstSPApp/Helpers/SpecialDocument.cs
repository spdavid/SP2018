using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFirstSPApp.Constants;
using OfficeDevPnP.Core.Entities;

namespace MyFirstSPApp.Helpers
{
    public class SpecialDocument
    {

        public static void CleanUpSpecialDocument(ClientContext ctx)
        {
            ctx.Web.GetListByTitle("Special Documents").DeleteObject();
            ctx.ExecuteQuery();

            ctx.Web.GetContentTypeById(SPConstants.SPECIAL_DOC_CT).DeleteObject();
            ctx.ExecuteQuery();

            ctx.Web.GetFieldById(SPConstants.FIELD_Approver_ID.ToGuid()).DeleteObject();
            ctx.Web.GetFieldById(SPConstants.FIELD_DESC_ID.ToGuid()).DeleteObject();
            ctx.Web.GetFieldById(SPConstants.FIELD_DOCOWNER_ID.ToGuid()).DeleteObject();
            ctx.Web.GetFieldById(SPConstants.FIELD_DOCTYPE_ID.ToGuid()).DeleteObject();
            ctx.Web.GetFieldById(SPConstants.FIELD_PUBDATE_ID.ToGuid()).DeleteObject();
            ctx.ExecuteQuery();


        }


        public static void CreateSpecialDocument(ClientContext ctx)
        {
            CleanUpSpecialDocument(ctx);
            if (!ctx.Web.ContentTypeExistsById(SPConstants.SPECIAL_DOC_CT))
            {
                ContentType specialDocContentType = ctx.Web.CreateContentType("Special Document", SPConstants.SPECIAL_DOC_CT, "Davids Content Types");
            }

            if (!ctx.Web.ListExists("Approvers"))
            {
                ctx.Web.CreateList(ListTemplateType.GenericList, "Approvers", false, true, "Lists/Approvers");
            }
                List approverList = ctx.Web.GetListByTitle("Approvers");
                string ApproversListId = approverList.Id.ToString();

            // Document Type Field
            if (!ctx.Web.FieldExistsById(SPConstants.FIELD_DOCTYPE_ID))
            {
                FieldCreationInformation fieldInfo = new FieldCreationInformation(FieldType.Choice);
                fieldInfo.Id = SPConstants.FIELD_DOCTYPE_ID.ToGuid();
                fieldInfo.InternalName = SPConstants.FIELD_DOCTYPE_INTERNAL;
                fieldInfo.DisplayName = "Document Type";
                fieldInfo.Group = "Davids Fields";
                var field = ctx.Web.CreateField<FieldChoice>(fieldInfo);
                field.Choices = new string[] { "Invoice", "Template", "Checklist", "Policy" };
                field.Update();
                ctx.ExecuteQueryRetry();
            }

            // Description Field
            if (!ctx.Web.FieldExistsById(SPConstants.FIELD_DESC_ID))
            {
                FieldCreationInformation fieldInfo = new FieldCreationInformation(FieldType.Note);
                fieldInfo.Id = SPConstants.FIELD_DESC_ID.ToGuid();
                fieldInfo.InternalName = SPConstants.FIELD_DESC_INTERNAL;
                fieldInfo.DisplayName = "Description";
                fieldInfo.Group = "Davids Fields";
                var field = ctx.Web.CreateField<FieldMultiLineText>(fieldInfo);
                field.RichText = true;
                field.Update();
                ctx.ExecuteQueryRetry();
            }

            // Pub Date Field
            if (!ctx.Web.FieldExistsById(SPConstants.FIELD_PUBDATE_ID))
            {
                FieldCreationInformation fieldInfo = new FieldCreationInformation(FieldType.DateTime);
                fieldInfo.Id = SPConstants.FIELD_PUBDATE_ID.ToGuid();
                fieldInfo.InternalName = SPConstants.FIELD_PUBDATE_INTERNAL;
                fieldInfo.DisplayName = "Publish Date";
                fieldInfo.Group = "Davids Fields";
                var field = ctx.Web.CreateField<FieldDateTime>(fieldInfo);
                field.DisplayFormat = DateTimeFieldFormatType.DateTime;
                field.DefaultValue = "[Today]";
                field.Update();
                ctx.ExecuteQueryRetry();
            }

            // Pub Date Field
            if (!ctx.Web.FieldExistsById(SPConstants.FIELD_PUBDATE_ID))
            {
                FieldCreationInformation fieldInfo = new FieldCreationInformation(FieldType.DateTime);
                fieldInfo.Id = SPConstants.FIELD_PUBDATE_ID.ToGuid();
                fieldInfo.InternalName = SPConstants.FIELD_PUBDATE_INTERNAL;
                fieldInfo.DisplayName = "Publish Date";
                fieldInfo.Group = "Davids Fields";
                var field = ctx.Web.CreateField<FieldDateTime>(fieldInfo);
                field.DisplayFormat = DateTimeFieldFormatType.DateTime;
                field.Update();
                ctx.ExecuteQueryRetry();
            }

            // Pub Date Field
            if (!ctx.Web.FieldExistsById(SPConstants.FIELD_DOCOWNER_ID))
            {
                FieldCreationInformation fieldInfo = new FieldCreationInformation(FieldType.User);
                fieldInfo.Id = SPConstants.FIELD_DOCOWNER_ID.ToGuid();
                fieldInfo.InternalName = SPConstants.FIELD_DOCOWNER_INTERNAL;
                fieldInfo.DisplayName = "Owner";
                fieldInfo.Group = "Davids Fields";
                var field = ctx.Web.CreateField<FieldUser>(fieldInfo);
                field.SelectionMode = FieldUserSelectionMode.PeopleOnly;
                field.Update();
                ctx.ExecuteQueryRetry();
            }

            // Pub Date Field
            if (!ctx.Web.FieldExistsById(SPConstants.FIELD_Approver_ID))
            {
                
                FieldCreationInformation fieldInfo = new FieldCreationInformation(FieldType.Lookup);
                fieldInfo.Id = SPConstants.FIELD_Approver_ID.ToGuid();
                fieldInfo.InternalName = SPConstants.FIELD_Approver_INTERNAL;
                fieldInfo.DisplayName = "Approver";
                fieldInfo.Group = "Davids Fields";
                var field = ctx.Web.CreateField<FieldLookup>(fieldInfo);
                field.LookupList = ApproversListId;
                field.LookupField = "Title";
                field.Update();
                ctx.ExecuteQueryRetry();
            }


            ctx.Web.AddFieldToContentTypeById(SPConstants.SPECIAL_DOC_CT, SPConstants.FIELD_DOCTYPE_ID);
            ctx.Web.AddFieldToContentTypeById(SPConstants.SPECIAL_DOC_CT, SPConstants.FIELD_DESC_ID);
            ctx.Web.AddFieldToContentTypeById(SPConstants.SPECIAL_DOC_CT, SPConstants.FIELD_PUBDATE_ID);
            ctx.Web.AddFieldToContentTypeById(SPConstants.SPECIAL_DOC_CT, SPConstants.FIELD_DOCOWNER_ID);
            ctx.Web.AddFieldToContentTypeById(SPConstants.SPECIAL_DOC_CT, SPConstants.FIELD_Approver_ID);

            ctx.Web.AddContentTypeToListById("Special Documents", SPConstants.SPECIAL_DOC_CT, true);

            ctx.Web.RemoveContentTypeFromListByName("Special Documents", "Document");

            if (!ctx.Web.ListExists("Special Documents")) 
            {
                List list = ctx.Web.CreateList(ListTemplateType.DocumentLibrary, "Special Documents", false, true, "SpecialDocuments", true);

                ctx.Load(list.DefaultView);
                ctx.ExecuteQueryRetry();

                list.DefaultView.ViewFields.Add(SPConstants.FIELD_DOCTYPE_INTERNAL);
                list.DefaultView.ViewFields.Add(SPConstants.FIELD_DESC_INTERNAL);
                list.DefaultView.ViewFields.Add(SPConstants.FIELD_PUBDATE_INTERNAL);
                list.DefaultView.ViewFields.Add(SPConstants.FIELD_DOCOWNER_INTERNAL);

                list.DefaultView.Update();
                ctx.ExecuteQueryRetry();

            }
            

           
           
           




        }


    }
}
