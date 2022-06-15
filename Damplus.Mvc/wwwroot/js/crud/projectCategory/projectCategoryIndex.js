$(document).ready(function () {
    //DataTable Start from Here

    $('#dataTables').DataTable({
        dom: "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        "order": [[5, "desc"]],
        buttons: [
            {
                text: 'Yeni P.Kateqoriya',
                attr: {
                    id: "btnAdd",
                },
                className: 'btn btn-success',
                action: function (e, dt, node, config) {

                }
            },
            {
                text: 'P.Kateqoriya Yenilə',
                className: 'btn btn-warning',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: '/Admin/ProjectCategory/GetAllCategories/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#dataTables').hide();
                            $('.btnUpdate').show();
                        },
                        success: function (data) {
                            const ProjectCategoryListDto = jQuery.parseJSON(data);
                            if (ProjectCategoryListDto.ResultStatus === 0) {
                                let tableBody = "";
                                $.each(ProjectCategoryListDto.Categories.$values,
                                    function (index, ProjectCategory) {
                                        tableBody += `
                                                <tr name=${ProjectCategory.Id}>
                                                        <td>${ProjectCategory.Id}</td>
                                                        <td>${ProjectCategory.Name}</td>
                                                        <td>${ProjectCategory.Description}</td>
                                                        <td class="center"><span class="status active">${ProjectCategory.IsActive ? "Bəli" : "Xeyr"}</span></td>
                                                        <td class="center"><span class="status active">${ProjectCategory.IsDeleted ? "Bəli" : "Xeyr"}</span></td>
                                                        <td class="center">${ProjectCategory.CreatedDate}</td>
                                                        <td class="center">${ProjectCategory.CreatedByName}</td>
                                                        <td class="center">${ProjectCategory.ModifiedDate}</td>
                                                        <td class="center">${ProjectCategory.ModifiedByName}</td>
                                                        <td>${ProjectCategory.Note}</td>
                                                        <td class="text-center">
                                                             <button class="btn btn-primary btn-sm btn-update" data-id="${ProjectCategory.Id}"><span class="fas fa-edit"></span> Edit</button>
                                                             <button class="btn btn-danger btn-sm btn-delete" data-id="${ProjectCategory.Id}"><span class="fas fa-minus-circle"></span> Sil</button>
                                                        </td>
                                                    </tr>
                                                 `
                                    });
                                $('#dataTables > tbody').replaceWith(tableBody);
                                $('.btnUpdate').hide();
                                $('#dataTables').fadeIn(1500);
                            } else {
                                toastr.error(`${ProjectCategoryListDto.Message}`, "Xeta Bas verdi");
                            }
                        },
                        error: function (err) {
                            $('.btnUpdate').hide();
                            $('#dataTables').fadeIn(1000);
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

    //AJAX Get/ Getting the _ProjectCategoryAddPartial Starts From Here

    $(function () {
        const url = '/Admin/ProjectCategory/Add/';
        const placeholderDiv = $('#modalPlaceHolder');
        $("#btnAdd").click(function () {
            $.get(url).done(function (data) {
                placeholderDiv.html(data);
                placeholderDiv.find(".modal").modal("show");
            });
        });

        //AJAX Get/ Getting the _ProjectCategoryAddPartial Ends  Here
        placeholderDiv.on('click', '#btnSave', function (event) {
            event.preventDefault();
            const form = $('#form-projectcategory-add');
            const actionUrl = form.attr('action');
            const dataToSend = form.serialize();
            $.post(actionUrl, dataToSend).done(function (data) {
                const ProjectCategoryAddAjaxModel = jQuery.parseJSON(data);
                const newFormBody = $('.modal-body', ProjectCategoryAddAjaxModel.ProjectCategoryPartial);
                placeholderDiv.find('.modal-body').replaceWith(newFormBody);
                const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                if (isValid) {
                    placeholderDiv.find('.modal').modal('hide');
                    const newTableRow = `
                             <tr class="odd" name="${ProjectCategoryAddAjaxModel.ProjectCategoryDto.ProjectCategory.Id}">
                                <td>${ProjectCategoryAddAjaxModel.ProjectCategoryDto.ProjectCategory.Id}</td>
                                <td>${ProjectCategoryAddAjaxModel.ProjectCategoryDto.ProjectCategory.Name}</td>
                                <td>${ProjectCategoryAddAjaxModel.ProjectCategoryDto.ProjectCategory.Description}</td>
                                <td class="center">${ProjectCategoryAddAjaxModel.ProjectCategoryDto.ProjectCategory.IsActive ? "Bəli" : "Xeyr"}</span></td>
                                <td class="center"><span class="status active">${ProjectCategoryAddAjaxModel.ProjectCategoryDto.ProjectCategory.IsDeleted ? "Bəli" : "Xeyr"}</span></td>
                                <td class="center">${ProjectCategoryAddAjaxModel.ProjectCategoryDto.ProjectCategory.CreatedDate}</td>
                                <td class="center">${ProjectCategoryAddAjaxModel.ProjectCategoryDto.ProjectCategory.CreatedByName}</td>
                                <td class="center">${ProjectCategoryAddAjaxModel.ProjectCategoryDto.ProjectCategory.ModifiedDate}</td>
                                <td class="center">${ProjectCategoryAddAjaxModel.ProjectCategoryDto.ProjectCategory.ModifiedByName}</td>
                                <td class="text-center">
                                    <button class="btn btn-primary btn-sm btn-update" data-id="${ProjectCategoryAddAjaxModel.ProjectCategoryDto.ProjectCategory.Id}"><span class="fas fa-edit"></span> Edit</button>
                                    <button class="btn btn-danger btn-sm btn-delete" data-id="${ProjectCategoryAddAjaxModel.ProjectCategoryDto.ProjectCategory.Id}"><span class="fas fa-minus-circle"></span> Sil</button>
                                </td>
                            </tr>`;
                    const newTableRowObject = $(newTableRow);
                    newTableRowObject.hide();
                    $('#dataTables').append(newTableRowObject);
                    newTableRowObject.fadeIn(3500);
                    toastr.success(`${ProjectCategoryAddAjaxModel.ProjectCategoryDto.Message}`, 'Uğurlu Əməliyyat');
                } else {
                    let summarytext = "";
                    $('#validation-summary > ul > li').each(function () {
                        let text = $(this).text();
                        summarytext += `*${text}\n`;
                    });
                    toastr.warning(summarytext);
                }
            });
        });

    });
        //Ajax POST / Posting the FormData as ProjectCategoryAddDto starts from here


    //AJAX Get/ Getting the _ProjectCategoryUpdatePaartial Starts From Here 

    $(function () {
        const url = '/Admin/ProjectCategory/Update/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '.btn-update', function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            $.get(url, { ProjectCategoryId: id }).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find('.modal').modal('show');
            }).fail(function () {
                toastr.error('Xeta');
            });
        });


        placeHolderDiv.on('click', "#btnSave", function (event) {
            event.preventDefault();
            const form = $('#form-ProjectCategory-update');
            const actionUrl = form.attr('action');
            const dataToSend = form.serialize(); //ProjectCategoryUpdateDto cevirir
            $.post(actionUrl, dataToSend, function (data) {
                console.log(data);
                const ProjectCategoryUpdateAjaxModel = jQuery.parseJSON(data);
                console.log(ProjectCategoryUpdateAjaxModel);
                const newFormBody = $('.modal-body', ProjectCategoryUpdateAjaxModel.ProjectCategoryUpdatePartial);
                placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                if (isValid) {
                    placeHolderDiv.find('.modal').modal('hide');
                    const newTableRow = `
                             <tr class="odd" name="${ProjectCategoryUpdateAjaxModel.ProjectCategoryDto.ProjectCategory.Id}">
                                <td>${ProjectCategoryUpdateAjaxModel.ProjectCategoryDto.ProjectCategory.Id}</td>
                                <td>${ProjectCategoryUpdateAjaxModel.ProjectCategoryDto.ProjectCategory.Fullname}</td>
                                <td>${ProjectCategoryUpdateAjaxModel.ProjectCategoryDto.ProjectCategory.Position}</td>
                                <td>${ProjectCategoryUpdateAjaxModel.ProjectCategoryDto.ProjectCategory.Photo}</td>
                                <td class="center">${ProjectCategoryUpdateAjaxModel.ProjectCategoryDto.ProjectCategory.IsActive ? "Bəli" : "Xeyr"}</span></td>
                                <td class="center"><span class="status active">${ProjectCategoryUpdateAjaxModel.ProjectCategoryDto.ProjectCategory.IsDeleted ? "Bəli" : "Xeyr"}</span></td>
                                <td class="center">${ProjectCategoryUpdateAjaxModel.ProjectCategoryDto.ProjectCategory.CreatedDate}</td>
                                <td class="center">${ProjectCategoryUpdateAjaxModel.ProjectCategoryDto.ProjectCategory.CreatedByName}</td>
                                <td class="center">${ProjectCategoryUpdateAjaxModel.ProjectCategoryDto.ProjectCategory.ModifiedDate}</td>
                                <td class="center">${ProjectCategoryUpdateAjaxModel.ProjectCategoryDto.ProjectCategory.ModifiedByName}</td>
                                <td class="text-center">
                                    <button class="btn btn-primary btn-sm btn-update" data-id="${ProjectCategoryUpdateAjaxModel.ProjectCategoryDto.ProjectCategory.Id}"><span class="fas fa-edit"></span> Edit</button>
                                    <button class="btn btn-danger btn-sm btn-delete" data-id="${ProjectCategoryUpdateAjaxModel.ProjectCategoryDto.ProjectCategory.Id}"><span class="fas fa-minus-circle"></span> Sil</button>
                                </td>
                            </tr>`;
                    const newTableRowObject = $(newTableRow);
                    const categoryTableRow = $(`[name="${ProjectCategoryUpdateAjaxModel.ProjectCategoryDto.ProjectCategory.Id}"]`);
                    newTableRowObject.hide();
                    categoryTableRow.replaceWith(newTableRowObject);
                    newTableRowObject.fadeIn(3500);
                    toastr.success(`${ProjectCategoryUpdateAjaxModel.ProjectCategoryDto.Message}`, "Uğurlu Əməliyyat!")
                } else {
                    let summarytext = "";
                    $('#validation-summary > ul > li').each(function () {
                        let text = $(this).text();
                        summarytext = `*${text}\n`;
                    });
                    toastr.warning(summarytext);
                };
            }).fail(function (response) {
                console.log(response);
            });
        });

    });
    
    //AJAX Get/ Getting the _ProjectCategoryAddPartial End From Here 

    //Ajax POST / Deleting a Category starts from here

    $(document).on('click', '.btn-delete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');
        const tableRow = $(`[name="${id}"]`);
        const ProjectCategoryName = tableRow.find('td:eq(1)').text();
        Swal.fire({
            title: 'Silmək istədiyinizdən əminsiniz?',
            text: `${ProjectCategoryName} adlı komanda uzvu silinəcəkdir!`,
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
                    data: { ProjectCategoryId: id },
                    url: '/Admin/ProjectCategory/Delete/',
                    success: function (data) {
                        const ProjectCategoryDto = jQuery.parseJSON(data);
                        if (ProjectCategoryDto.ResultStatus === 0) {
                            Swal.fire(
                                'Kateqoriya silindi!',
                                `${ProjectCategoryDto.ProjectCategory.Fullname} adlı komanda uzvu uğurla silindi`,
                                'success'
                            );
                            tableRow.fadeOut(3500);
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Xəta Baş Verdi...',
                                text: `${ProjectCategoryDto.Message}`,
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
    //Ajax POST / Deleting a Category ends from here


})