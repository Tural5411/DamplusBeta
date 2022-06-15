$(document).ready(function () {
    //DataTable Start from Here

    $('#dataTables').DataTable({
        dom: "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        "order": [[5, "desc"]],
        buttons: [
            {
                text: 'Yeni Xidmet',
                attr: {
                    id: "btnAdd",
                },
                className: 'btn btn-success',
                action: function (e, dt, node, config) {

                }
            },
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


    $(function () {
        const url = '/Admin/Business/Update/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '.btn-update', function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            $.get(url, { businessId: id }).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find('.modal').modal('show');
            }).fail(function () {
                toastr.error('Xeta');
            });
        });


        placeHolderDiv.on('click', "#btnSave", function (event) {
            event.preventDefault();
            const form = $('#form-business-update');
            const actionUrl = form.attr('action');
            const dataToSend = form.serialize(); //businessUpdateDto cevirir
            $.post(actionUrl, dataToSend, function (data) {
                console.log(data);
                const businessUpdateAjaxModel = jQuery.parseJSON(data);
                console.log(businessUpdateAjaxModel);
                const newFormBody = $('.modal-body', businessUpdateAjaxModel.BusinessUpdatePartial);
                placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                if (isValid) {
                    placeHolderDiv.find('.modal').modal('hide');
                    const newTableRow = `
                             <tr class="odd" name="${businessUpdateAjaxModel.BusinessDto.Business.Id}">
                                <td>${businessUpdateAjaxModel.BusinessDto.Business.Id}</td>
                                <td>${businessUpdateAjaxModel.BusinessDto.Business.Header}</td>
                                <td>${businessUpdateAjaxModel.BusinessDto.Business.Description}</td>
                                <td>${businessUpdateAjaxModel.BusinessDto.Business.Icon}</td>
                                <td class="center">${businessUpdateAjaxModel.BusinessDto.Business.IsActive ? "Bəli" : "Xeyr"}</span></td>
                                <td class="center"><span class="status active">${businessUpdateAjaxModel.BusinessDto.Business.IsDeleted ? "Bəli" : "Xeyr"}</span></td>
                                <td class="center">${businessUpdateAjaxModel.BusinessDto.Business.CreatedDate}</td>
                                <td class="center">${businessUpdateAjaxModel.BusinessDto.Business.CreatedByName}</td>
                                <td class="center">${businessUpdateAjaxModel.BusinessDto.Business.ModifiedDate}</td>
                                <td class="center">${businessUpdateAjaxModel.BusinessDto.Business.ModifiedByName}</td>
                                <td class="text-center">
                                    <button class="btn btn-primary btn-sm btn-update" data-id="${businessUpdateAjaxModel.BusinessDto.Business.Id}"><span class="fas fa-edit"></span> Edit</button>
                                    <button class="btn btn-danger btn-sm btn-delete" data-id="${businessUpdateAjaxModel.BusinessDto.Business.Id}"><span class="fas fa-minus-circle"></span> Sil</button>
                                </td>
                            </tr>`;
                    const newTableRowObject = $(newTableRow);
                    const categoryTableRow = $(`[name="${businessUpdateAjaxModel.BusinessDto.Business.Id}"]`);
                    newTableRowObject.hide();
                    categoryTableRow.replaceWith(newTableRowObject);
                    newTableRowObject.fadeIn(3500);
                    toastr.success(`${businessUpdateAjaxModel.BusinessDto.Message}`, "Uğurlu Əməliyyat!")
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
    
    //AJAX Get/ Getting the _BusinessAddPartial End From Here 

    //Ajax POST / Deleting a Category starts from here

    $(document).on('click', '.btn-delete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');
        const tableRow = $(`[name="${id}"]`);
        const businessName = tableRow.find('td:eq(1)').text();
        console.log(businessName);
        console.log(tableRow);
        Swal.fire({
            title: 'Silmək istədiyinizdən əminsiniz?',
            text: `${businessName} adlı kateqoriya silinəcəkdir!`,
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
                    data: { businessId: id },
                    url: '/Admin/Business/Delete/',
                    success: function (data) {
                        const businessDto = jQuery.parseJSON(data);
                        if (businessDto.ResultStatus === 0) {
                            Swal.fire(
                                'Kateqoriya silindi!',
                                `${businessDto.Business.Header} adlı kateqoriya uğurla silindi`,
                                'success'
                            );
                            tableRow.fadeOut(3500);
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Xəta Baş Verdi...',
                                text: `${businessDto.Message}`,
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