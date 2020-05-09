def traffic_bill():
	traffic = 0.0
	f = open('Damp.txt')
	for line in f.readlines():
		if len(line.split("217.15.20.194")) == 1:
			continue
		try:
			traffic += float(line.split()[-2])
		except ValueError:
			traffic += float(line.split()[-3]) * 1024.0 * 1024
	traffic /= 1024
	traffic -= 1000
	return round(traffic, 2)