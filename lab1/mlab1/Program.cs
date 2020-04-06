using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;

namespace Tarification
{
class Abonent
{
public int Number;
public double CallDurationIncoming = 0;
public double CallDurationOutcoming = 0;
public int SmsCount = 0;
}
class Program
{
static StreamReader streamReader;
static CsvReader csvReader;
static void Main(string[] args)
{
System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
var abonent = GetAbonent();
var tarification = ComputeTarification(abonent);
Console.WriteLine("Исходящие ={0} Входящие={1} sms={2}", abonent.CallDurationOutcoming, abonent.CallDurationIncoming, abonent.SmsCount);
Console.WriteLine("Итоговая тарификация абонента \"{0}\" = {1}", abonent.Number, tarification);
}

static Abonent GetAbonent()
{
var abonent = new Abonent();
abonent.Number = 968247916;

OpenCsv();
while (csvReader.Read())
{
if(csvReader.GetField("msisdn_origin") == "968247916")
{
abonent.CallDurationOutcoming = abonent.CallDurationOutcoming + Convert.ToDouble(csvReader.GetField("call_duration"));
abonent.SmsCount = abonent.SmsCount + int.Parse(csvReader.GetField("sms_number"));
}
if(csvReader.GetField("msisdn_dest") == "968247916")
{
abonent.CallDurationIncoming = abonent.CallDurationIncoming + Convert.ToDouble(csvReader.GetField("call_duration"));
}
}
CloseCsv();

return abonent;
}

static void OpenCsv()
{
string pathCsv = "/home/lyumos/Документы/mlab1/data.csv";

streamReader = new StreamReader(pathCsv);
csvReader = new CsvReader(streamReader, System.Globalization.CultureInfo.InvariantCulture);
csvReader.Configuration.Delimiter = ",";
csvReader.Read();
csvReader.ReadHeader();
}

static void CloseCsv()
{
csvReader = null;
streamReader.Close();
}

static double ComputeTarification(Abonent abonent)
{
double sum = 0;
sum = sum + abonent.SmsCount * 1;
sum = sum + abonent.CallDurationIncoming * 1;
sum = sum + abonent.CallDurationOutcoming * 3;

return sum;
}
}
}