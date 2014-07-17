/**
 * editor_plugin_src.js
 *
 * Copyright 2009, Moxiecode Systems AB
 * Released under LGPL License.
 *
 * License: http://tinymce.moxiecode.com/license
 * Contributing: http://tinymce.moxiecode.com/contributing
 */

(function () {
    tinymce.create('tinymce.plugins.KeywordManagerPlugin', {
        init: function (ed, url) {

            ed.addCommand('mceKeywordManager', function () {

                var keywordEditor = $(".KeywordEditor");

                var htmlEditor = ed;
                var selectedHtml = htmlEditor.selection.getContent();

                if (!String.IsNullOrEmpty(selectedHtml)) {

                    if (keywordEditor.length) {
                        keywordEditor[0].KeywordEditor.Add(selectedHtml);
                    }
                }
            });

            // Register buttons
            ed.addButton('KeywordManager', {
                title: 'Dodaj kljuènu rijeè',
                cmd: 'mceKeywordManager',
                image: url + '/img/icon-AddKeyword.png'
            });
        },

        getInfo: function () {
            return {
                longname: 'KeywordManager',
                author: 'David Saiæ',
                authorurl: '',
                infourl: '',
                version: tinymce.majorVersion + "." + tinymce.minorVersion
            };
        }
    });

    // Register plugin
    tinymce.PluginManager.add('KeywordManager', tinymce.plugins.KeywordManagerPlugin);
})();