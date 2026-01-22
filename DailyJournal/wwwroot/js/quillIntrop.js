window.quillEditor = {
    init: function () {
        new Quill('#editor', {
            theme: 'snow',
            placeholder: 'Start writing...',
            modules: {
                toolbar: [
                    ['bold', 'italic', 'underline'],
                    [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                    ['clean']
                ]
            }
        });
    },

    getContent: function () {
        return document.querySelector('#editor .ql-editor').innerHTML;
    }
};
