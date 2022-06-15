$(document).ready(function () {
    /* DataTables start here. */
    const dataTable = $('#deletedCategoriesTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                text: 'Yenilə',
                className: 'btn btn-warning',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: '/Admin/Category/GetAllDeletedCategories/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#deletedCategoriesTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const deletedCategories = jQuery.parseJSON(data);
                            dataTable.clear();
                            console.log(deletedCategories);
                            if (deletedCategories.ResultStatus === 0) {
                                $.each(deletedCategories.Categories.$values,
                                    function (index, category) {
                                        const newTableRow = dataTable.row.add([
                                            category.Id,
                                            category.Name,
                                            category.Description,
                                            category.IsActive ? "Bəli" : "Xeyr",
                                            category.IsDeleted ? "Bəli" : "Xeyr",
                                            category.Note,
                                            category.CreatedDate,
                                            category.CreatedByName,
                                            category.ModifiedDate,
                                            category.ModifiedByName,
                                            `
                                <button class="btn btn-warning btn-sm btn-undo" data-id="${category.Id}"><span class="fas fa-undo"></span></button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${category.Id}"><span class="fas fa-minus-circle"></span></button>
                            `
                                        ]).node();
                                        const jqueryTableRow = $(newTableRow);
                                        jqueryTableRow.attr('name', `${category.Id}`);
                                    });
                                dataTable.draw();
                                $('.spinner-border').hide();
                                $('#deletedCategoriesTable').fadeIn(1400);
                            } else {
                                toastr.error(`${deletedCategories.Categories.Message}`, 'İşlem Başarısız!');
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#deletedCategoriesTable').fadeIn(1000);
                            toastr.error(`${err.responseText}`, 'Hata!');
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
    });

    /* DataTables end here */

    /* Ajax POST / UndoDelete a Category starts from here */

    $(document).on('click',
        '.btn-undo',
        function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            const tableRow = $(`[name="${id}"]`);
            let categoryName = tableRow.find('td:eq(1)').text();
            Swal.fire({
                title: 'Arxivdən geri gətirmək istədiyinizdən əminsiniz?',
                text: `${categoryName} adlı kateqoriya arxivdən geri gətiriləcəkdir!`,
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Bəli, arxivdən geri gətirmək istəyirəm.',
                cancelButtonText: 'Xeyr, istəmirəm.'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        data: { categoryId: id },
                        url: '/Admin/Category/UndoDelete/',
                        success: function (data) {
                            const undoDeletedCategoryResult = jQuery.parseJSON(data);
                            console.log(undoDeletedCategoryResult);
                            if (undoDeletedCategoryResult.ResultStatus===0) {
                                Swal.fire(
                                    'Arxivdən Geri Gətirildi!',
                                    `${undoDeletedCategoryResult.Message}`,
                                    'success'
                                );
                                dataTable.row(tableRow).remove().draw();
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Uğursuz Əməliyyat!',
                                    text: `${undoDeletedCategoryResult.Message}`,
                                });
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            toastr.error(`${err.responseText}`, "Xəta!");
                        }
                    });
                }
            });
        });
    /* Ajax POST / UndoDelete a Category ends  here */

    /* Ajax POST / HardDelete a Category starts from here */

    $(document).on('click',
        '.btn-delete',
        function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            const tableRow = $(`[name="${id}"]`);
            let categoryName = tableRow.find('td:eq(1)').text();
            Swal.fire({
                title: 'Ömürlük silmək istədiyinizdən əminsiniz?',
                text: `${categoryName} adlı kateqoriya ömürlük olara silinəcəkdir!`,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Bəli, ömürlük silmək istəyirəm.',
                cancelButtonText: 'Xeyr, istəmirəm.'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        data: { categoryId: id },
                        url: '/Admin/Category/HardDelete/',
                        success: function (data) {
                            const hardDeleteCategoryResult = jQuery.parseJSON(data);
                            if (hardDeleteCategoryResult.ResultStatus === 0) {
                                Swal.fire(
                                    'Ömürlük olaraq databazadan silindi!',
                                    `${hardDeleteCategoryResult.Data.Message}`,
                                    'success'
                                );
                                dataTable.row(tableRow).remove().draw();
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Uğursuz Əməliyyat!',
                                    text: `${hardDeleteCategoryResult.Data.Message}`,
                                });
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            toastr.error(`${err.responseText}`, "Xəta!");
                        }
                    });
                }
            });
        });
    /* Ajax POST / HardDelete a Category ends  here */
});