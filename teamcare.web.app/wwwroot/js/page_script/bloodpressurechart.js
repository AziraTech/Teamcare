var daterange = $("#txtdaterangeblood").val();

$.ajax({
	type: "GET",
	url: '/BloodPressure/GetBloodReadingChartData',
	data: { id: $('#hdnserviceuserid').val(), daterange: daterange },
	success: function (data) {
		if (data && data.statuscode === 1) {
			var systolic = [], diastolic = [], pulse = [];
			var category = [];
			maxValue = 25;
			data.data.forEach(function (element) {
				if (element.systolicReading > maxValue) {
					maxValue = element.systolicReading;
				}
				if (element.diastolicReading > maxValue) {
					maxValue = element.diastolicReading;
				}
				if (element.pulse > maxValue) {
					maxValue = element.pulse;
				}
				category.push(element.testDate);
				systolic.push(element.systolicReading);
				diastolic.push(element.diastolicReading);
				pulse.push(element.pulse);
			});
			var options = {
				series: [
					{
						name: "SYSTOLIC READING",
						data: systolic //[28, 29, 33, 36, 32, 32, 33]
					},
					{
						name: "DIASTOLIC READING",
						data: diastolic //[12, 11, 14, 18, 17, 13, 13]
					},
					{
						name: "PULSE",
						data: pulse //[56, 40, 75, 78, 79, 80, 90]
					}
				],
				chart: {
					height: 500,
					type: 'line',
					dropShadow: {
						enabled: true,
						color: '#000',
						top: 18,
						left: 7,
						blur: 10,
						opacity: 0.2
					},
					toolbar: {
						show: false
					}
				},
				colors: ['#77B6EA', '#545454', '#787898'],
				dataLabels: {
					enabled: true,
				},
				stroke: {
					curve: 'smooth'
				},
				title: {
					text: 'Blood Pressure daily',
					align: 'left'
				},
				grid: {
					borderColor: '#e7e7e7',
					row: {
						colors: ['#f3f3f3', 'transparent'], // takes an array which will be repeated on columns
						opacity: 0.5
					},
				},
				markers: {
					size: 1
				},
				xaxis: {
					categories: category,//['16-Jan', '17-Jan', '18-Jan', '19-Jan', '20-Jan', '21-Jan', '22-Jan'],
					title: {
						text: 'Day'
					}
				},
				yaxis: {
					title: {
						text: 'Blood Pressure'
					},
					min: 0,
					max: (maxValue + 10)
				},
				legend: {
					position: 'top',
					horizontalAlign: 'right',
					floating: true,
					offsetY: -25,
					offsetX: -5
				}
			};

			var chart = new ApexCharts(document.querySelector("#kt_charts_widget_2_chart"), options);
			chart.render();
		}
	}
});


