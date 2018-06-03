/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';

    //thangnq
    //config.extraPlugins = 'syntaxhightlight';
    config.syntaxhightlight_lang = 'csharp';
    config.syntaxhightlight_hideControls = true;
    config.language = 'vi';
    config.filebrowserBrowseUrl = '/Assets/admin/js/plugin/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl ='/Assets/admin/js/plugin/ckfinder/ckfinder.html?Type=Images'
    config.filebrowserFlashBrowseUrl = '/Assets/admin/js/plugin/ckfinder/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl =
        '/Assets/admin/js/plugin/ckfinder/core/connector/aspx/connector.aspx?command=Quickupload&type=files';
    config.filebrowserImageUploadUrl = '/Data';
    config.filebrowserFlashUploadUrl = '/Assets/admin/js/plugin/ckfinder/core/connector/aspx/connector.aspx?command=Quickupload&type=flash';

};
