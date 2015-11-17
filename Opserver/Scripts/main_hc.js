$(document).ready(function({

    $('#container').highcharts({
        chart: {
            type: 'area',
			zoomType: 'x'
        },
         title: {
            text: "Snapshot"
        },
        xAxis: {
            type: 'datetime',
             title: {
                text: "Snapshot ID"
            }
        },
        yAxis: {
            title: {
                text: "Value"
            }
        },

        series: [{
            data: @json_string,
            name: 'Sessions'
        }]
    });

});