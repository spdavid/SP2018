using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Entities;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client.Taxonomy;
using OfficeDevPnP.Core.Pages;


namespace MyFirstSPApp.Helpers
{
    public class SPHelper
    {
        public static void ShowWebTitle(ClientContext ctx)
        {
            Web web = ctx.Web;

            // gets all properties of the web. Better to get only what you need. 
            //ctx.Load(web);

            // tell the context to order the web. but only the Title and the WebTemplate Properties
            ctx.Load(web, w => w.Title, w => w.WebTemplate);

            //ctx.Load(ctx.Web.CurrentUser);

            // tell the context to execute your orders
            ctx.ExecuteQuery();

            Console.WriteLine(web.Title);
        }

        public static void ChangeWebTitle(ClientContext ctx, string newTitle)
        {
            // you dont need to fetch the object if you are only setting new values (most of the time)
            //ctx.Load(ctx.Web, w => w.Title);
            //ctx.ExecuteQuery();

            // change the title
            ctx.Web.Title = newTitle;
            // make sure you update it
            ctx.Web.Update();

            // tell sharepoint to do the work
            ctx.ExecuteQuery();
        }

        public static void GetLists(ClientContext ctx)
        {
            ListCollection lists = ctx.Web.Lists;

            // gets only thte non hidden lists and only returns title. Ca 2 seconds
            ctx.Load(lists,
                l => l.Where(list => list.Hidden == false),
                l => l.Include(list => list.Title));
            // gets only thte non hidden lists and returns all properites. Ca 4 seconds
            // first way is better
            //ctx.Load(lists,
            //    l => l.Where(list => list.Hidden == false));

            ctx.ExecuteQuery();

            foreach (var list in lists)
            {
                Console.WriteLine(list.Title);
            }
        }


        public static void CreateList(ClientContext ctx)
        {

            ListCreationInformation info = new ListCreationInformation();
            info.Title = "Davids List";
            info.Url = "lists/davidslist";
            info.TemplateType = 100;

            ctx.Web.Lists.Add(info);

            ctx.ExecuteQuery();

            //Console.WriteLine("list created. press enter to delete it");
            //Console.ReadLine();

            //List list = ctx.Web.Lists.GetByTitle("Davids List");
            //list.DeleteObject();

            //ctx.ExecuteQuery();



        }

        public static void CreateListItem(ClientContext ctx)
        {
            List list = ctx.Web.Lists.GetByTitle("Davids List");

            for (int i = 1; i <= 10; i++)
            {
                ListItem item = list.AddItem(new ListItemCreationInformation());

                item["Title"] = "New List Item " + i.ToString();
                item.Update();
            }

            ctx.ExecuteQuery();
        }

        public static void CreateCarContetType(ClientContext ctx)
        {
            string VehicleContentType = "0x0100567A676ED8534670A79990FADE68DE47";
            string carContentType =     "0x0100567A676ED8534670A79990FADE68DE4700DC042CAC4BCA4EDFB9334B39999E4576";

            if (!ctx.Web.ContentTypeExistsById(VehicleContentType))
            {
                ctx.Web.CreateContentType("Vehicle", VehicleContentType, "Davids Content Types");
            }

            if (!ctx.Web.ContentTypeExistsById(carContentType))
            {
                ctx.Web.CreateContentType("Car", carContentType, "Davids Content Types");
            }

            Guid carModleFieldId = "{6DAD1B3E-A841-4E35-8B00-0E68E269C1F5}".ToGuid();
            if (!ctx.Web.FieldExistsById(carModleFieldId))
            {
                FieldCreationInformation modleFieldInfo = new FieldCreationInformation(FieldType.Text);
                modleFieldInfo.Id = carModleFieldId;
                modleFieldInfo.InternalName = "F_CarModel";
                modleFieldInfo.DisplayName = "Car Model";
                modleFieldInfo.Group = "Davids Fields";

                ctx.Web.CreateField(modleFieldInfo);
            }

            ctx.Web.AddFieldToContentTypeById(carContentType, carModleFieldId.ToString());

            Guid carYearFieldId = "{AB76F05D-FC5F-4456-989D-F435BBF39230}".ToGuid();
            if (!ctx.Web.FieldExistsById(carYearFieldId))
            {
                FieldCreationInformation yearFieldInfo = new FieldCreationInformation(FieldType.Number);
                yearFieldInfo.Id = carYearFieldId;
                yearFieldInfo.InternalName = "F_CarYear";
                yearFieldInfo.DisplayName = "Car Year";
                yearFieldInfo.Group = "Davids Fields";

                ctx.Web.CreateField(yearFieldInfo);
            }

            ctx.Web.AddFieldToContentTypeById(carContentType, carYearFieldId.ToString());

            Guid carColorFieldId = "{3A2B1887-ABBF-4EDB-BCCC-11CF48A279EA}".ToGuid();
            if (!ctx.Web.FieldExistsById(carColorFieldId))
            {
                FieldCreationInformation colorFieldInfo = new FieldCreationInformation(FieldType.Choice);
                colorFieldInfo.Id = carColorFieldId;
                colorFieldInfo.InternalName = "F_CarColor";
                colorFieldInfo.DisplayName = "Car Color";
                colorFieldInfo.Group = "Davids Fields";

                FieldChoice field = ctx.Web.CreateField<FieldChoice>(colorFieldInfo);
                field.Choices = new string[] { "Green", "Blue", "Red" };
                field.DefaultValue = "Green";
                field.Update();
                ctx.ExecuteQuery();
            }
            ctx.Web.AddFieldToContentTypeById(carContentType, carColorFieldId.ToString());
            ctx.Web.CreateList(ListTemplateType.GenericList, "Cars", false, true, "lists/cars", true);
            ctx.Web.AddContentTypeToListById("Cars", carContentType, true);

        }

        public static void CreateFieldFromXML(ClientContext ctx)
        {
            string dirtyfieldXML = @"
                <Field Type='UserMulti' 
                       DisplayName='xUser' 
                       List='UserInfo' 
                       Required='FALSE' 
                       EnforceUniqueValues='FALSE' 
                       ShowField='ImnName' 
                       UserSelectionMode='PeopleAndGroups' 
                       UserSelectionScope='0' 
                       Mult='TRUE' 
                       Sortable='FALSE' 
                       Group='Custom Columns' 
                       ID='{829a241d-1c2f-4b1d-9e8b-271a8533c002}' 
                       SourceID='{06433904-afa4-4d08-92c9-412d90a3fff3}' 
                       StaticName='xUser' 
                       Name='xUser' 
                       Version='1' />";

            string cleanfieldXML = @"
                <Field Type='UserMulti' 
                       DisplayName='new User' 
                       List='UserInfo' 
                       Required='FALSE' 
                       EnforceUniqueValues='FALSE' 
                       ShowField='ImnName' 
                       UserSelectionMode='PeopleAndGroups' 
                       UserSelectionScope='0' 
                       Mult='TRUE' 
                       Sortable='FALSE' 
                       Group='Davids Columns' 
                       ID='{F48D6010-E0B9-418E-AC1F-817220756CD4}' 
                       StaticName='newUser' 
                       Name='newUser' />";


            ctx.Web.CreateFieldsFromXMLString(cleanfieldXML);


        }

        public static void CreateFieldsFromXMLFile(ClientContext ctx)
        {
            string xmlFilePath = @"C:\Users\DavidOpdendries\source\SP2018\OfficeDev1\MyFirstSPApp\MyFirstSPApp\XML\Fields.xml";
            ctx.Web.CreateFieldsFromXMLFile(xmlFilePath);
        }

        public static void CreateFieldsFromProvisioningXML(ClientContext ctx)
        {
            string folderPath = @"C:\Users\DavidOpdendries\source\SP2018\OfficeDev1\ProvisioningXMLExamples\";
            XMLTemplateProvider provider =
              new XMLFileSystemTemplateProvider(folderPath, "");

            XMLSharePointTemplateProvider spProvider = new XMLSharePointTemplateProvider(ctx, "https://folkis2018.sharepoint.com/sites/David/", "Shared%20Documents");
            
            var provisioningTemplate = spProvider.GetTemplate("template.xml");
            
            ctx.Web.ApplyProvisioningTemplate(provisioningTemplate);



        }


        public static void GetAllGreenCars(ClientContext ctx, string color)
        {
            List list = ctx.Web.GetListByTitle("Cars");
            CamlQuery query = new CamlQuery();

            string s = "test " + color + " sdfsdf";

            string xml = @"<View>
                            <Query>
                                <Where>
                                    <Eq>
                                       <FieldRef Name='F_CarColor' />
                                        <Value Type='Choice'>" + color + @"</Value>
                                    </Eq>
                                </Where>
                                <OrderBy>
                                    <FieldRef Name='F_CarYear' Ascending='FALSE' />
                                </OrderBy>
                            </Query>
                            <ViewFields>
                                <FieldRef Name='LinkTitle' />
                                <FieldRef Name='F_CarModel' />
                                <FieldRef Name='F_CarYear' />
                                <FieldRef Name='F_CarColor' />
                            </ViewFields>
                            <RowLimit>2</RowLimit>
                        </View>";

            query.ViewXml = xml;
           
            ListItemCollection items = list.GetItems(query);

            ctx.Load(items);
            ctx.ExecuteQuery();




            foreach (var item in items)
            {
                Console.WriteLine(item["Title"]);
                Console.WriteLine(item["F_CarYear"]);
                Console.WriteLine(item["F_CarColor"]);

            }

        }

        public static void CreateTaxonomy(ClientContext ctx)
        {
          TermStore store = ctx.Site.GetDefaultSiteCollectionTermStore();
           
            TermGroup group = store.GetGroup("{0671B184-F909-4DCA-B383-6E05C969BEF1}".ToGuid());
            ctx.Load(group);
            ctx.ExecuteQuery();
            if (group.ServerObjectIsNull())
            {
                group = store.CreateTermGroup("David1", "{0671B184-F909-4DCA-B383-6E05C969BEF1}".ToGuid());
            }

            TermSet ts = group.EnsureTermSet("Color", "{19419441-6982-4978-8800-F12A46E6FE37}".ToGuid(), 1033);

            // Term term = ts.CreateTerm("Green", 1033, "{FA6628FA-9790-4622-9B54-23D103A67FCD}".ToGuid());
            // Term term2 = ts.CreateTerm("Röd", 1053, "{24FA5756-25E8-4257-A4E5-D53C3D48B595}".ToGuid());
            // ctx.ExecuteQuery();

            // term2.EnsureLabel(1033, "Red", false);

            Term term2 = ts.CreateTerm("Blue", 1033, "{73E90F88-BD96-4066-8237-2D0DAAE3F11C}".ToGuid());
            ctx.ExecuteQuery();

            term2.EnsureLabel(1053, "Blå", false);
        }

        public static void CreatePage(ClientContext ctx)
        {
            var oldpage = ctx.Web.LoadClientSidePage("david2.aspx");
            oldpage.Delete();
            ctx.ExecuteQuery();

            List carsList = ctx.Web.GetListByTitle("Cars");
            ctx.Load(ctx.Web);
            ctx.ExecuteQuery();

           

            var page = ctx.Web.AddClientSidePage("david2.aspx", true);
            page.PageTitle = "Davids Page";
            page.AddSection(CanvasSectionTemplate.TwoColumn, 1);
            page.AddSection(CanvasSectionTemplate.ThreeColumn, 2);


            var components = page.AvailableClientSideComponents();
            var component = components.Where(c => c.Name == "e377ea37-9047-43b9-8cdb-a761be2f8e09").FirstOrDefault();

            ClientSideWebPart wp = new ClientSideWebPart(component);

          //  page.AddControl(wp, -1);

            var column = page.Sections[1].Columns[2];

            page.AddControl(wp, column, 1);

            var props = "{'isDocumentLibrary':false,'selectedListId':'" + carsList.Id.ToString() + "','listTitle':'Cars','selectedListUrl':'" + ctx.Web.ServerRelativeUrl + "/Lists/cars','webRelativeListUrl':'/Lists/cars','webpartHeightKey':4}";

            var ListComponent = components.Where(c => c.Name == "f92bf067-bc19-489e-a556-7fe95f508720").FirstOrDefault();

           ClientSideWebPart wp2 = new ClientSideWebPart(ListComponent);
            wp2.PropertiesJson = props;
            var column2 = page.Sections[0].Columns[1];

            page.AddControl(wp2, column2, 1);

            page.Save();

            ctx.ExecuteQuery();



        }


    }
}
