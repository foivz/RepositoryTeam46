/// <reference path="../../../../../../Scripts/_references.js" />
/**
 * editor_plugin_src.js
 *
 */

(function () {
    tinymce.create('tinymce.plugins.ResourcePickerPlugin', {
        init: function (ed, url) {

            ed.addCommand('mceResourcePicker', function () {

                var resourcePickerUrl = null;

                try {
                    resourcePickerUrl = String.Format("{0}?tinymceId={1}", Seds.Web.Config.ResourcePickerUrl, ed.editorId);

                } catch (e) {

                    resourcePickerUrl = String.Format("/Admin/ResourcePicker.aspx?tinymceId={0}", ed.editorId);
                }

                SadaCore.UI.Popup.Open(resourcePickerUrl);
            });

            ed.addButton('ResourcePicker', {
                title: 'Odabir datoteke',
                cmd: 'mceResourcePicker',
                image: url + '/img/icon-browse.png'
            });
        },
        insert: function (ed, resourceData) {

            var dom = ed.dom;

            var html = null;

            if (resourceData.Type == SadaCore.UI.ResourceType.Image) {

                html = dom.createHTML('img', {
                    src: resourceData.PreviewUrl,
                    alt: resourceData.Name,
                    title: resourceData.Name,
                    border: 0
                });
            }
            else if (resourceData.Type == SadaCore.UI.ResourceType.Audio) {

                html = String.Format(_audioPlayerHtml, resourceData.PreviewUrl);
            }

            ed.execCommand('mceInsertContent', false, html);

            SadaCore.UI.Popup.Close();
        },
        getInfo: function () {
            return {
                longname: 'ResourcePicker',
                author: 'David Saiæ',
                authorurl: '',
                infourl: '',
                version: tinymce.majorVersion + "." + tinymce.minorVersion
            };
        }
    });

    tinymce.PluginManager.add('ResourcePicker', tinymce.plugins.ResourcePickerPlugin);
})(tinymce);