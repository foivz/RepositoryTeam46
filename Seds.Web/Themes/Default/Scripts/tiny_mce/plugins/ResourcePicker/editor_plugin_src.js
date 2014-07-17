/**
 * editor_plugin_src.js
 *
 * Copyright 2009, Moxiecode Systems AB
 * Released under LGPL License.
 *
 * License: http://tinymce.moxiecode.com/license
 * Contributing: http://tinymce.moxiecode.com/contributing
 */

(function() {
    tinymce.create('tinymce.plugins.ResourceBrowsePlugin', {
		init : function(ed, url) {
			
		    ed.addCommand('mceResourceBrowser', function () {
				
				if (ed.dom.getAttrib(ed.selection.getNode(), 'class').indexOf('mceItem') != -1)
					return;

				ed.windowManager.open({
				    file: url + '/ResourceBrowse.aspx',
					width : 480 + parseInt(ed.getLang('advimage.delta_width', 0)),
					height : 385 + parseInt(ed.getLang('advimage.delta_height', 0)),
					inline : 1
				}, {
					plugin_url : url
				});
			});

			// Register buttons
			ed.addButton('resource', {
				title : 'Odabir datoteke',
				cmd: 'mceResourceBrowser'
			});
		},

		getInfo : function() {
			return {
				longname : 'ResourceBrowser',
				author : 'David Saiæ',
				authorurl : '',
				infourl : '',
				version : tinymce.majorVersion + "." + tinymce.minorVersion
			};
		}
	});

	// Register plugin
    tinymce.PluginManager.add('resourceBrowser', tinymce.plugins.ResourceBrowsePlugin);
})();