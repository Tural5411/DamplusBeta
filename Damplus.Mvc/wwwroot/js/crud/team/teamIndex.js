$(document).ready(function () {
    //DataTable Start from Here

    $('#dataTables').DataTable({
        dom: "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        "order": [[5, "desc"]],
        buttons: [
            {
                text: 'Yeni Komanda',
                attr: {
                    id: "btnAdd",
                },
                className: 'btn btn-success',
                action: function (e, dt, node, config) {

                }
            },
            {
                text: 'Komandani Yenilə',
                className: 'btn btn-warning',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: '/Admin/Team/GetAllCategories/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#dataTables').hide();
                            $('.btnUpdate').show();
                        },
                        success: function (data) {
                            const TeamListDto = jQuery.parseJSON(data);
                            if (TeamListDto.ResultStatus === 0) {
                                let tableBody = "";
                                $.each(TeamListDto.Categories.$values,
                                    function (index, Team) {
                                        tableBody += `
                                                <tr name=${Team.Id}>
                                                        <td>${Team.Id}</td>
                                                        <td>${Team.Name}</td>
                                                        <td>${Team.Description}</td>
                                                        <td class="center"><span class="status active">${Team.IsActive ? "Bəli" : "Xeyr"}</span></td>
                                                        <td class="center"><span class="status active">${Team.IsDeleted ? "Bəli" : "Xeyr"}</span></td>
                                                        <td class="center">${Team.CreatedDate}</td>
                                                        <td class="center">${Team.CreatedByName}</td>
                                                        <td class="center">${Team.ModifiedDate}</td>
                                                        <td class="center">${Team.ModifiedByName}</td>
                                                        <td>${Team.Note}</td>
                                                        <td class="text-center">
                                                             <button class="btn btn-primary btn-sm btn-update" data-id="${Team.Id}"><span class="fas fa-edit"></span> Edit</button>
                                                             <button class="btn btn-danger btn-sm btn-delete" data-id="${Team.Id}"><span class="fas fa-minus-circle"></span> Sil</button>
                                                        </td>
                                                    </tr>
                                                 `
                                    });
                                $('#dataTables > tbody').replaceWith(tableBody);
                                $('.btnUpdate').hide();
                                $('#dataTables').fadeIn(1500);
                            } else {
                                toastr.error(`${TeamListDto.Message}`, "Xeta Bas verdi");
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

    //AJAX Get/ Getting the _TeamAddPartial Starts From Here

    $(function () {
        const url = '/Admin/Team/Add/';
        const placeholderDiv = $('#modalPlaceHolder');
        $("#btnAdd").click(function () {
            $.get(url).done(function (data) {
                placeholderDiv.html(data);
                placeholderDiv.find(".modal").modal("show");
            });
        });

        //AJAX Get/ Getting the _TeamAddPartial Ends  Here
        placeholderDiv.on('click', '#btnSave', function (event) {
            event.preventDefault();
            const form = $('#form-team-add');
            const actionUrl = form.attr('action');
            const dataToSend = form.serialize();
            $.post(actionUrl, dataToSend).done(function (data) {
                const TeamAddAjaxModel = jQuery.parseJSON(data);
                const newFormBody = $('.modal-body', TeamAddAjaxModel.TeamAddPartial);
                placeholderDiv.find('.modal-body').replaceWith(newFormBody);
                const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                if (isValid) {
                    placeholderDiv.find('.modal').modal('hide');
                    const newTableRow = `
                             <tr class="odd" name="${TeamAddAjaxModel.TeamDto.Team.Id}">
                                <td>${TeamAddAjaxModel.TeamDto.Team.Id}</td>
                                <td>${TeamAddAjaxModel.TeamDto.Team.Fullname}</td>
                                <td>${TeamAddAjaxModel.TeamDto.Team.Position}</td>
                                <td>${TeamAddAjaxModel.TeamDto.Team.Photo}</td>
                                <td class="center">${TeamAddAjaxModel.TeamDto.Team.IsActive ? "Bəli" : "Xeyr"}</span></td>
                                <td class="center"><span class="status active">${TeamAddAjaxModel.TeamDto.Team.IsDeleted ? "Bəli" : "Xeyr"}</span></td>
                                <td class="center">${TeamAddAjaxModel.TeamDto.Team.CreatedDate}</td>
                                <td class="center">${TeamAddAjaxModel.TeamDto.Team.CreatedByName}</td>
                                <td class="center">${TeamAddAjaxModel.TeamDto.Team.ModifiedDate}</td>
                                <td class="center">${TeamAddAjaxModel.TeamDto.Team.ModifiedByName}</td>
                                <td class="text-center">
                                    <button class="btn btn-primary btn-sm btn-update" data-id="${TeamAddAjaxModel.TeamDto.Team.Id}"><span class="fas fa-edit"></span> Edit</button>
                                    <button class="btn btn-danger btn-sm btn-delete" data-id="${TeamAddAjaxModel.TeamDto.Team.Id}"><span class="fas fa-minus-circle"></span> Sil</button>
                                </td>
                            </tr>`;
                    const newTableRowObject = $(newTableRow);
                    newTableRowObject.hide();
                    $('#dataTables').append(newTableRowObject);
                    newTableRowObject.fadeIn(3500);
                    toastr.success(`${TeamAddAjaxModel.TeamDto.Message}`, 'Uğurlu Əməliyyat');
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
        //Ajax POST / Posting the FormData as TeamAddDto starts from here


    //AJAX Get/ Getting the _TeamUpdatePaartial Starts From Here 

    $(function () {
        const url = '/Admin/Team/Update/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '.btn-update', function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            $.get(url, { teamId: id }).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find('.modal').modal('show');
            }).fail(function () {
                toastr.error('Xeta');
            });
        });


        placeHolderDiv.on('click', "#btnSave", function (event) {
            event.preventDefault();
            const form = $('#form-team-update');
            const actionUrl = form.attr('action');
            const dataToSend = form.serialize(); //TeamUpdateDto cevirir
            $.post(actionUrl, dataToSend, function (data) {
                console.log(data);
                const TeamUpdateAjaxModel = jQuery.parseJSON(data);
                console.log(TeamUpdateAjaxModel);
                const newFormBody = $('.modal-body', TeamUpdateAjaxModel.TeamUpdatePartial);
                placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                if (isValid) {
                    placeHolderDiv.find('.modal').modal('hide');
                    const newTableRow = `
                             <tr class="odd" name="${TeamUpdateAjaxModel.TeamDto.Team.Id}">
                                <td>${TeamUpdateAjaxModel.TeamDto.Team.Id}</td>
                                <td>${TeamUpdateAjaxModel.TeamDto.Team.Fullname}</td>
                                <td>${TeamUpdateAjaxModel.TeamDto.Team.Position}</td>
                                <td>${TeamUpdateAjaxModel.TeamDto.Team.Photo}</td>
                                <td class="center">${TeamUpdateAjaxModel.TeamDto.Team.IsActive ? "Bəli" : "Xeyr"}</span></td>
                                <td class="center"><span class="status active">${TeamUpdateAjaxModel.TeamDto.Team.IsDeleted ? "Bəli" : "Xeyr"}</span></td>
                                <td class="center">${TeamUpdateAjaxModel.TeamDto.Team.CreatedDate}</td>
                                <td class="center">${TeamUpdateAjaxModel.TeamDto.Team.CreatedByName}</td>
                                <td class="center">${TeamUpdateAjaxModel.TeamDto.Team.ModifiedDate}</td>
                                <td class="center">${TeamUpdateAjaxModel.TeamDto.Team.ModifiedByName}</td>
                                <td class="text-center">
                                    <button class="btn btn-primary btn-sm btn-update" data-id="${TeamUpdateAjaxModel.TeamDto.Team.Id}"><span class="fas fa-edit"></span> Edit</button>
                                    <button class="btn btn-danger btn-sm btn-delete" data-id="${TeamUpdateAjaxModel.TeamDto.Team.Id}"><span class="fas fa-minus-circle"></span> Sil</button>
                                </td>
                            </tr>`;
                    const newTableRowObject = $(newTableRow);
                    const categoryTableRow = $(`[name="${TeamUpdateAjaxModel.TeamDto.Team.Id}"]`);
                    newTableRowObject.hide();
                    categoryTableRow.replaceWith(newTableRowObject);
                    newTableRowObject.fadeIn(3500);
                    toastr.success(`${TeamUpdateAjaxModel.TeamDto.Message}`, "Uğurlu Əməliyyat!")
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
    
    //AJAX Get/ Getting the _TeamAddPartial End From Here 

    //Ajax POST / Deleting a Category starts from here

    $(document).on('click', '.btn-delete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');
        const tableRow = $(`[name="${id}"]`);
        const teamName = tableRow.find('td:eq(1)').text();
        Swal.fire({
            title: 'Silmək istədiyinizdən əminsiniz?',
            text: `${teamName} adlı komanda uzvu silinəcəkdir!`,
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
                    data: { TeamId: id },
                    url: '/Admin/Team/Delete/',
                    success: function (data) {
                        const TeamDto = jQuery.parseJSON(data);
                        if (TeamDto.ResultStatus === 0) {
                            Swal.fire(
                                'Kateqoriya silindi!',
                                `${TeamDto.Team.Fullname} adlı komanda uzvu uğurla silindi`,
                                'success'
                            );
                            tableRow.fadeOut(3500);
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Xəta Baş Verdi...',
                                text: `${TeamDto.Message}`,
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