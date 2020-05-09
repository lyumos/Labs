def mobile_bill():
	f = open('data.csv')
	incoming_minutes = 0
	outcoming_minutes = 0
	sms = 0
	for line in f.readlines():
		if line.split(',')[1] == '968247916':
			outcoming_minutes += float(line.split(',')[3])
			sms += int(line.split(',')[4])
		if line.split(',')[2] == '968247916':
			incoming_minutes += float(line.split(',')[3])
	return outcoming_minutes * 3 + incoming_minutes + sms