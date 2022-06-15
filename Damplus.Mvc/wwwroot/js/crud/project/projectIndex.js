$(document).ready(function () {

    //DataTable Start from Here
    const datatable = $('#projectTables').DataTable({
        dom: "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                
                text: 'Yeni Proyekt',
                attr: {
                    id: "btnAdd"
                },
                className: 'btn btn-success',
                action: function (e, dt, node, config) {
                    let url = window.location.href;
                    url = url.replace("/Index", "");
                    window.open(`${url}/Add`,"_self");
                }
            },
            {
                text: 'Yenilə',
                className: 'btn btn-warning',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: '/Admin/Project/GetAllArticles/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#articleTables').hide();
                            $('.btnUpdate').show();
                        },
                        success: function (data) {
                            const articleResult = jQuery.parseJSON(data);
                            datatable.clear();
                            if (articleResult.Data.ResultStatus === 0) {
                                let categoriesArray = [];
                                $.each(articleResult.Data.Articles.$values,
                                    function (index, article) {
                                        const newArticle = getJsonNetObject(article, articleResult.Data.Articles.$values);
                                        let newCategory = getJsonNetObject(newArticle.Category, newArticle);
                                        if (newCategory !== null) {
                                            categoriesArray.push(newCategory);
                                        }
                                        if (newCategory === null) {
                                            newCategory = categoriesArray.find((category) => {
                                                return category.$id === newArticle.Category.$ref;
                                            });
                                        }
                                        console.log(newCategory);
                                        const newTableRow = datatable.row.add([
                                            newArticle.Id,
                                            newArticle.Title,
                                            newCategory.Name,
                                            newArticle.Date,
                                            `<img src="/img/${newArticle.Thumbnail}" class="my-image-table" alt="${newArticle.Title}" />`,
                                            newArticle.CreatedByName,
                                            newArticle.ViewCount,
                                            newArticle.IsActive,
                                            newArticle.IsDeleted,
                                            `<td class="text-center">
                                                <button class="btn btn-primary btn-sm btn-update" data-id="${newArticle.Id}"><span class="fas fa-edit"></span> </button>
                                                <button class="btn btn-danger btn-sm btn-delete" data-id="${newArticle.Id}"><span class="fas fa-minus-circle"></span> </button>
                                            </td>`
                                        ]).node();
                                        const jqueryTableRow = $(newTableRow);//Jquery obyektine cevirdiy
                                        jqueryTableRow.attr('name', `${newArticle.Id}`);
                                    });
                                datatable.draw();
                                $('.btnUpdate').hide();
                                $('#articleTables').fadeIn(1500);
                            } else {
                                toastr.error(`${articleResult.Message}`, "Xeta Bas verdi");
                            }
                        },
                        error: function (err) {
                            $('.btnUpdate').hide();
                            $('#articleTables').fadeIn(1000);
                            toastr.error(`${err.responseText}`, "Xeta!");
                        }
                    });
                }
            }

        ],
        "language": {
            "sEmptyTable": "Cədvəldə heç bir məlumat yoxdur",
            "sInfo": " _TOTAL_ Nəticədən _START_ - _END_ Arası Nəticələr",
            "lengthMenu": "Səhifədə _MENU_ Nəticə Göstər",
            "sZeroRecords": "Nəticə Tapılmadı.",
            "sInfoEmpty": "Nəticə Yoxdur",
            "sInfoFiltered": "( _MAX_ Nəticə İçindən Tapılanlar)",
            "sInfoPostFix": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Yüklənir...",
            "sProcessing": "Gözləyin...",
            "sSearch": "Axtarış:",
            "sZeroRecords": "Nəticə Tapılmadı.",
            "oPaginate": {
                "sFirst": "İlk",
                "sLast": "Axırıncı",
                "sNext": "Sonraki",
                "sPrevious": "Öncəki"
            },

            "oAria": {
                "sSortAscending": ": sütunu artma sırası üzərə aktiv etmək",
                "sSortDescending": ": sütunu azalma sırası üzərə aktiv etmək"
            }

        },
        "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
        "Paginate": true,
        "bLengthChange": true,
        "bFilter": true,
        "bSort": false,
        "bInfo": true,
        "bAutoWidth": false,
        "placeholder": " ",
    });
    //DataTables End from Here

    //Ajax POST / Deleting a User starts from here

    $(document).on('click', '.btn-delete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');
        const tableRow = $(`[name="${id}"]`);
        const articleTitle = tableRow.find('td:eq(1)').text(); //2 = 2ci indexe sahib deyeri al
        Swal.fire({
            title: 'Silmək istədiyinizdən əminsiniz?',
            text: `${articleTitle} adlı məqalə silinəcəkdir!`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Bəli, silmək istəyirəm.',
            cancelButtonText: 'Xeyr, silmək istəmirəm.'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    data: { articleId: id },
                    url: '/Admin/Project/Delete/',
                    success: function (data) {
                        const articleResult = jQuery.parseJSON(data);
                        if (articleResult.ResultStatus === 0) {
                            Swal.fire(
                                'Məqalə silindi!',
                                `${articleResult.Message}`,
                                'success'
                            );
                            datatable.row(tableRow).remove().draw();
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Xəta Baş Verdi...',
                                text: `${articleResult.Message}`,
                            });
                        };
                    },
                    error: function (err) {
                        toastr.error(`${err.responseText}`, "Xeta");
                    }
                });
            };
        });
    });
    //Ajax POST / Deleting a User ends from here

    //Document Ready ends here
});