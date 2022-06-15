$(document).ready(function () {
    $("#dataTableCategory").DataTable();
    //Chart
    $.get('/Admin/Article/GetAllByViewCount/?isAscending=false&takeSize=10', function (data) {
        const articleResult = jQuery.parseJSON(data);
        let viewCountContext = $('#viewCountChart');
        let viewCountChart = new Chart(viewCountContext,
            {
                type: 'bar',
                data: {
                    labels: articleResult.$values.map(article => article.Title),
                    datasets: [
                        {
                            label: "Oxunma Sayı",
                            data: articleResult.$values.map(article => article.ViewsCount),
                            backgroundColor: '#fb3640',
                            hoverBorderWith: 4,
                            hoverBorderColor: 'black'
                        },
                        {
                            label: "Şərh Sayı",
                            data: articleResult.$values.map(article => article.CommentCount),
                            backgroundColor: '#fdca40',
                            hoverBorderWith: 4,
                            hoverBorderColor: 'black'
                        }],
                    options: {
                        plugins: {
                            legend: {
                                labels: {
                                    font: {
                                        size: 25,

                                    }
                                }
                            }
                        }
                    }
                },
            })
    })
})