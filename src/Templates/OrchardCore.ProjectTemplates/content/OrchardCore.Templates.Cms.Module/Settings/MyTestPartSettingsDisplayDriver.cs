using System;
using System.Threading.Tasks;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Templates.Cms.Module.Models;

namespace OrchardCore.Templates.Cms.Module.Settings
{
    public class MyTestPartSettingsDisplayDriver : ContentPartDefinitionDisplayDriver
    {
        public override IDisplayResult Edit(ContentPartDefinition contentPartDefinition)
        {
            if (!String.Equals(nameof(MyTestPart), contentPartDefinition.Name))
            {
                return null;
            }

            return Initialize<MyTestPartSettingsViewModel>("MyTestPartSettings_Edit", model =>
            {
                var settings = contentPartDefinition.GetSettings<MyTestPartSettings>();

                model.MySetting = settings.MySetting;
                model.MyTestPartSettings = settings;
            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentPartDefinition contentPartDefinition, UpdatePartEditorContext context)
        {
            if (!String.Equals(nameof(MyTestPart), contentPartDefinition.Name))
            {
                return null;
            }

            var model = new MyTestPartSettingsViewModel();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.MySetting))
            {
                context.Builder.WithSettings(new MyTestPartSettings { MySetting = model.MySetting });
            }

            return Edit(contentPartDefinition);
        }
    }
}
