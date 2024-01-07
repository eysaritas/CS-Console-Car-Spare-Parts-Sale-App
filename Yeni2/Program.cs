using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.ComponentModel.Design;
using System.Reflection;
using System.Net.Sockets;

//ENSAR YİĞİT SARITAŞ - 220229027
namespace Yeni2
{
	class Araba
	{
        public string Brand { get; set; }
        public string Model { get; set; }
		public string Packet { get; set; }
        public string SpareParts { get; set; }
        public int PartCounts { get; set; }
    }
	class User : Araba
	{
		public void Register(string registertype)
		{
			string username;
			string password;
			string email;
			string phone;
			string registerType = registertype;

			Console.WriteLine("Kullanıcı Kayıt Formu: \n");

			do
			{
				Console.Write("Kullanıcı adınızı giriniz: ");
				username = Console.ReadLine();
			}
			while (!checkUsername(username));

			do
			{
				Console.Write("Şifrenizi giriniz: ");
				password = Console.ReadLine();
			}
			while (!checkPassword(password));

			do
			{
				Console.Write("E-postanızı giriniz: ");
				email = Console.ReadLine();
			}
			while (!checkEmail(email));

			do
			{
				Console.Write("Telefon numaranızı giriniz: ");
				phone = Console.ReadLine();
			}
			while (!checkPhone(phone));

			UsersFiles(username, password, email, phone, registerType);

			Console.WriteLine("\nKayıt başarıyla tamamlandı!");
		}
		public void UsersFiles(string username, string password, string email, string phone, string registerType)
		{
			string folderName;

			if (registerType.ToLower() == "customer")
			{
				folderName = "MüsteriKayıtlar";
				if (!Directory.Exists(folderName))
					Directory.CreateDirectory(folderName);

				string fileName = Path.Combine(folderName, $"{username}.txt");

				using (StreamWriter sw = File.CreateText(fileName))
				{
					sw.WriteLine($"User Adı: {username}");
					sw.WriteLine($"Şifre: {password}");
					sw.WriteLine($"E-posta: {email}");
					sw.WriteLine($"Telefon: {phone}");
				}
			}
			else if (registerType.ToLower() == "dealer")
			{
				folderName = "SaticiKayitlar";
				if (!Directory.Exists(folderName))
					Directory.CreateDirectory(folderName);

				string fileName = Path.Combine(folderName, $"{username}.txt");

				using (StreamWriter sw = File.CreateText(fileName))
				{
					sw.WriteLine($"User Adı: {username}");
					sw.WriteLine($"Şifre: {password}");
					sw.WriteLine($"E-posta: {email}");
					sw.WriteLine($"Telefon: {phone}");
				}
			}
			else if (registerType.ToLower() == "admin")
			{
				folderName = "YoneticiKayitlar";
				if (!Directory.Exists(folderName))
					Directory.CreateDirectory(folderName);

				string fileName = Path.Combine(folderName, $"{username}.txt");

				using (StreamWriter sw = File.CreateText(fileName))
				{
					sw.WriteLine($"User Adı: {username}");
					sw.WriteLine($"Şifre: {password}");
					sw.WriteLine($"E-posta: {email}");
					sw.WriteLine($"Telefon: {phone}");
				}
			}
		}
		public int Login(string registerType)
		{
			Console.WriteLine("Kullanıcı Girişi\n");

			Console.Write("Kullanıcı Adı: ");
			string username = Console.ReadLine();

			Console.Write("Şifre: ");
			string password = Console.ReadLine();

			if (registerType == "customer")
			{
				string fileName = $@"C:\\Users\\Ensar's PC\\Desktop\\lab3dönem\\Yeni2\\Yeni2\\bin\\Debug\\MüsteriKayıtlar\\{username}.txt";
				if (File.Exists(fileName))
				{
					string[] lines = File.ReadAllLines(fileName);

					if (4 <= lines.Length && lines[0].Split(':')[1].Trim() == username && lines[1].Split(':')[1].Trim() == password)
					{
						Console.WriteLine("Giriş Başarılı! Hoşgeldiniz!");
						return 1;
					}
				}
				Console.WriteLine("Giriş Başarısız! Kullanıcı adı veya şifre hatalı! Tekrar Deneyiniz!");
				return 0;
			}
			if (registerType == "dealer")
			{
				string fileName = $@"C:\\Users\\Ensar's PC\\Desktop\\lab3dönem\\Yeni2\\Yeni2\\bin\\Debug\\SaticiKayitlar\\{username}.txt";
				if (File.Exists(fileName))
				{
					string[] lines = File.ReadAllLines(fileName);

					if (lines.Length == 4 && lines[0].Split(':')[1].Trim() == username && lines[1].Split(':')[1].Trim() == password)
					{
						Console.WriteLine("Giriş Başarılı! Hoşgeldiniz!");
						return 1;
					}
				}
				Console.WriteLine("Giriş Başarısız! Kullanıcı adı veya şifre hatalı!");
				return 0;
			}
			if (registerType == "admin")
			{
				string fileName = $@"C:\\Users\\Ensar's PC\\Desktop\\lab3dönem\\Yeni2\\Yeni2\\bin\\Debug\\YoneticiKayitlar\\{username}.txt";
				if (File.Exists(fileName))
				{
					string[] lines = File.ReadAllLines(fileName);

					if (lines.Length == 4 && lines[0].Split(':')[1].Trim() == username && lines[1].Split(':')[1].Trim() == password)
					{
						Console.WriteLine("Giriş Başarılı! Hoşgeldiniz!");
						return 1;
					}
				}
				Console.WriteLine("Giriş Başarısız! Kullanıcı adı veya şifre hatalı!");
				return 0;
			}
			return 0;
		}
		public static bool checkUsername(string username)
		{
			string usernameRegex = "^[a-zA-Z0-9]+$";

			if (string.IsNullOrEmpty(username))
			{
				Console.WriteLine("\nHatalı kullanıcı adı girdiniz. Kullanıcı adı boş olamaz!\n");
				return false;
			}
			if (!(5 <= username.Length && username.Length <= 20))
			{
				Console.WriteLine("\nHatalı kullanıcı adı girdiniz. Kullanıcı adı en az 5, en fazla 20 karakter içermelidir!\n");
				return false;
			}
			if (!(('A' <= username[0] && username[0] <= 'Z') || ('a' <= username[0] && username[0] <= 'z')))
			{
				Console.WriteLine("\nHatalı kullanıcı adı girdiniz. Kullanıcı adının ilk karakteri alfabetik karakter olmalıdır!\n");
				return false;
			}
			if (!Regex.IsMatch(username, usernameRegex))
			{
				Console.WriteLine("\nHatalı kullanıcı adı girdiniz. Kullanıcı adı yalnızca nümerik veya alfabetik karakterler içerebilir!\n");
				return false;
			}
			return true;
		}
		public static bool checkPassword(string password)
		{
			if (string.IsNullOrEmpty(password))
			{
				Console.WriteLine("\nHatalı şifre girdiniz. Şifre adı boş olamaz!\n");
				return false;
			}
			if (!(8 <= password.Length && password.Length <= 20))
			{
				Console.WriteLine("\nHatalı şifre girdiniz. Şifre en az 8, en fazla 20 karakter içermelidir!\n");
				return false;
			}
			if (!password.Any(char.IsDigit))
			{
				Console.WriteLine("\nHatalı şifre girdiniz. Şifre en az bir rakam içermelidir!\n");
				return false;
			}
			if (!password.Any(char.IsUpper))
			{
				Console.WriteLine("\nHatalı şifre girdiniz. Şifre en az bir büyük harf içermelidir!");
				return false;
			}
			if (!password.Any(char.IsLower))
			{
				Console.WriteLine("\nHatalı şifre girdiniz. Şifre en az bir küçük harf içermelidir!");
				return false;
			}
			if (!password.Any(c => "!@#$%&*-+".Contains(c)))
			{
				Console.WriteLine("\nHatalı şifre girdiniz. Şifre !@#$%&*-+ karakterlerinden en az birini içermelidir!");
				return false;
			}
			if (password.Contains(" "))
			{
				Console.WriteLine("\nHatalı şifre girdiniz. Şifre boşluk içeremez!");
				return false;
			}
			return true;
		}
		public static bool checkEmail(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				Console.WriteLine("\nHatalı e-posta girdiniz. E-posta adı boş olamaz!\n");
				return false;
			}
			if (!(('A' <= email[0] && email[0] <= 'Z') || ('a' <= email[0] && email[0] <= 'z') || ('0' <= email[0] && email[0] <= '9')))
			{
				Console.WriteLine("\nHatalı e-posta girdiniz. E-posta özel karakterle başlamamalıdır!");
				return false;
			}
			if (!email.Any(c => "@".Contains(c)))
			{
				Console.WriteLine("\nHatalı e-posta girdiniz. E-posta @ işareti içermelidir!");
				return false;
			}
			return true;
		}
		public static bool checkPhone(string phone)
		{
			string phoneRegex = @"^\d{3}-\d{3}-\d{4}$";
			string phoneAlphabetRegex = "^[a-zA-Z]$";
			if (string.IsNullOrEmpty(phone))
			{
				Console.WriteLine("\nHatalı telefon numarası girdiniz. Telefon numarası boş olamaz!\n");
				return false;
			}
			if (!Regex.IsMatch(phone, phoneRegex))
			{
				Console.WriteLine("\nHatalı telefon numarası girdiniz. Telefon numarası XXX-XXX-XXXX formatında olmalıdır!");
				return false;
			}
			if (Regex.IsMatch(phone, phoneAlphabetRegex))
			{
				Console.WriteLine("\nHatalı telefon numarası girdiniz. Telefon numarası hiçbir harfi içermemelidir!");
				return false;
			}
			return true;
		}
	}
	class Customer : User
	{
		public void list_cars()
		{
			string[] bilgiler = File.ReadAllLines("arabaBilgileri.txt");
			string[] parts;
			int count = 0;

			Console.WriteLine("1-Toyota, Corolla");
			Console.WriteLine("2-Honda, Civic");
			Console.WriteLine("3-Ford, Focus");
			Console.WriteLine("4-Volkswagen, Golf");
			Console.WriteLine("5-Chevrolet, Malibu");
			Console.WriteLine("6-BMW, 3 Serisi");
			Console.WriteLine("7-Mercedes-Benz, E Class");
			Console.WriteLine("8-Nissan, Altima");
			Console.WriteLine("9-Hyundai, Elantra");
			Console.WriteLine("10-Audi, A4");
			Console.WriteLine("Araç model bilgisi için seçim yapınız: ");
			string choose1 = Console.ReadLine();

			switch (choose1)
			{
				case "1":
					Console.Clear();
					Console.WriteLine("Donanım Paketi Seçiniz: ");
					Console.WriteLine("1-Vision");
					Console.WriteLine("2-Dream Multidrive");
					string choose2 = Console.ReadLine();

					switch (choose2)
					{
						case "1":
							Console.Clear();
							foreach (string line in bilgiler)
							{

								parts = line.Split(',');

								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");

								if (count++ == 4)
									break;

							}

							break;
						case "2":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");

								if (count++ == 4)
									break;
							}
							break;
					}
					break;

				case "2":
					Console.Clear();
					Console.WriteLine("Donanım Paketi Seçiniz: ");
					Console.WriteLine("1-Sport");
					Console.WriteLine("2-Technology");
					string choose3 = Console.ReadLine();

					switch (choose3)
					{
						case "1":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");
								if (count++ == 4)
									break;
							}
							break;

						case "2":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");
								if (count++ == 4)
									break;
							}
							break;
					}
					break;
				case "3":
					Console.Clear();
					Console.WriteLine("Donanım Paketi Seçiniz: ");
					Console.WriteLine("1-Titanium");
					Console.WriteLine("2-Winter");
					string choose4 = Console.ReadLine();

					switch (choose4)
					{
						case "1":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");
								if (count++ == 4)
									break;
							}

							break;
						case "2":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");
								if (count++ == 4)
									break;
							}
							break;
					}
					break;
				case "4":
					Console.Clear();
					Console.WriteLine("Donanım Paketi Seçiniz: ");
					Console.WriteLine("1-RLine");
					Console.WriteLine("2-Comfortline");
					string choose5 = Console.ReadLine();

					switch (choose5)
					{
						case "1":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");
								if (count++ == 4)
									break;
							}

							break;
						case "2":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");
								if (count++ == 4)
									break;
							}
							break;
					}
					break;
				case "5":
					Console.Clear();
					Console.WriteLine("Donanım Paketi Seçiniz: ");
					Console.WriteLine("1-LT");
					Console.WriteLine("2-Premier");
					string choose6 = Console.ReadLine();

					switch (choose6)
					{
						case "1":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");
								if (count++ == 4)
									break;
							}

							break;
						case "2":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");
								if (count++ == 4)
									break;
							}
							break;
					}
					break;
				case "6":
					Console.Clear();
					Console.WriteLine("Donanım Paketi Seçiniz: ");
					Console.WriteLine("1-M Sport");
					Console.WriteLine("2-Luxury");
					string choose7 = Console.ReadLine();

					switch (choose7)
					{
						case "1":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");
								if (count++ == 4)
									break;
							}

							break;
						case "2":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");
								if (count++ == 4)
									break;
							}
							break;
					}
					break;
				case "7":
					Console.Clear();
					Console.WriteLine("Donanım Paketi Seçiniz: ");
					Console.WriteLine("1-AMG");
					Console.WriteLine("2-Premium");
					string choose8 = Console.ReadLine();

					switch (choose8)
					{
						case "1":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");
								if (count++ == 4)
									break;
							}

							break;
						case "2":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");
								if (count++ == 4)
									break;
							}
							break;
					}
					break;
				case "8":
					Console.Clear();
					Console.WriteLine("Donanım Paketi Seçiniz: ");
					Console.WriteLine("1-SL");
					Console.WriteLine("2-SV");
					string choose9 = Console.ReadLine();

					switch (choose9)
					{
						case "1":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");
								if (count++ == 4)
									break;
							}

							break;
						case "2":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");
								if (count++ == 4)
									break;
							}
							break;
					}
					break;
				case "9":
					Console.Clear();
					Console.WriteLine("Donanım Paketi Seçiniz: ");
					Console.WriteLine("1-Limited");
					Console.WriteLine("2-Convenience");
					string choose10 = Console.ReadLine();

					switch (choose10)
					{
						case "1":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");
								if (count++ == 4)
									break;
							}

							break;
						case "2":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");
								if (count++ == 4)
									break;
							}
							break;
					}
					break;
				case "10":
					Console.Clear();
					Console.WriteLine("Donanım Paketi Seçiniz: ");
					Console.WriteLine("1-S Line");
					Console.WriteLine("2-Advanced");
					string choose11 = Console.ReadLine();

					switch (choose11)
					{
						case "1":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");
								if (count++ == 4)
									break;
							}

							break;
						case "2":
							Console.Clear();
							foreach (string line in bilgiler)
							{
								parts = line.Split(',');
								if (parts.Length >= 5)
									Console.WriteLine($"Yedek Parça: {parts[3]}, Envanter Sayısı: {parts[4]}");
								if (count++ == 4)
									break;
							}
							break;
					}
					break;
			}

		}
		public void AddCart()
		{
			Console.WriteLine("Yedek Parça Satın Alma Paneli: ");
			Console.WriteLine("Kullanıcı adınızı giriniz: ");
			string username = Console.ReadLine();

			Console.WriteLine("Satın almak istediğiniz parçanın araç markasını giriniz: ");
			string Wbrand = Console.ReadLine();

			Console.WriteLine("Satın almak istediğiniz parçanın araç modelini giriniz: ");
			string Wmodel = Console.ReadLine();

			Console.WriteLine("Satın almak istediğiniz parçanın araç donanım paketini giriniz: ");
			string Wpacket = Console.ReadLine();

			Console.WriteLine("Satın almak istediğiniz parçanın adını giriniz: ");
			string Wspareparts = Console.ReadLine();

			Console.WriteLine("Kaç adet satın almak istiyorsunuz? ");
			string Wnumber = Console.ReadLine();

			string fileSource = "arabaBilgileri.txt";
			string fileDestination = $@"C:\\Users\\Ensar's PC\\Desktop\\lab3dönem\\Yeni2\\Yeni2\\bin\\Debug\\MüsteriSepetBilgileri\\{username}SepetBilgi.txt";

			List<string> SourceUpdatedList = new List<string>();
			List<string> DestinationUpdatedList = new List<string>();

			using (StreamReader sr = new StreamReader(fileSource))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					string[] CarListInfos = line.Split(',');

					string Sbrand = CarListInfos[0].Trim();
					string Smodel = CarListInfos[1].Trim();
					string Spacket = CarListInfos[2].Trim();
					string Sspare = CarListInfos[3].Trim();
					string Snumber = CarListInfos[4].Trim();

					if (Wbrand == Sbrand && Wmodel == Smodel && Wpacket == Spacket && Wspareparts == Sspare)
					{
						CarListInfos[4] = Wnumber;
						DestinationUpdatedList.Add(string.Join(",", CarListInfos));
						int n1 = int.Parse(Snumber);
						int n2 = int.Parse(Wnumber);
						CarListInfos[4] = (n1 - n2).ToString();
						SourceUpdatedList.Add(string.Join(",", CarListInfos));
					}
					else
					{
						SourceUpdatedList.Add(line);
					}
				}
			}
			using (StreamWriter sw1 = new StreamWriter(fileSource))
			{
				foreach (string row in SourceUpdatedList)
				{
					sw1.WriteLine(row);
				}
			}
			using (StreamWriter sw2 = new StreamWriter(fileDestination))
			{
				foreach (string row in DestinationUpdatedList)
				{
					sw2.WriteLine(row);
				}
			}
		}
		public bool ShowApprovedProducts()
		{
			string[] brands = { "Toyota", "Honda", "Ford", "Volkswagen", "Chevrolet", "BMW", "Mercedes-Benz", "Nissan", "Hyundai", "Audi" };
			Console.WriteLine("Siparişi verdiğiniz kullanıcı adınızı giriniz: ");
			string username = Console.ReadLine();
			string DestinationLocation = $@"C:\\Users\\Ensar's PC\\Desktop\\lab3dönem\\Yeni2\\Yeni2\\bin\\Debug\\MüsteriKayıtlar\\{username}.txt";
            string SourceLocation = $@"C:\\Users\\Ensar's PC\\Desktop\\lab3dönem\\Yeni2\\Yeni2\\bin\\Debug\\MüsteriSepetBilgileri\\{username}SepetBilgi.txt";
            string[] products = File.ReadAllLines(DestinationLocation);

			foreach (string product in products)
			{
				string[] infos = product.Split(',');
				for (int i = 0; i < brands.Length; i++)
				{ 
					if (4 <= products.Length && infos[0] == brands[i])
					{
						Console.WriteLine($"{product} siparişiniz satıcı tarafından ONAYLANMIŞTIR.");
						return true;
					}
				}
			}
			if (!File.Exists(SourceLocation))
				Console.WriteLine("Siparişiniz satıcı tarafından ONAYLANMAMIŞTIR.");
			return false;
		}
		public void ShowAllProducts()
		{
			string[] bilgiler = File.ReadAllLines("arabaBilgileri.txt");

			foreach (string line in bilgiler)
			{
				Console.WriteLine(line);
			}
		}
        public void DeleteCustomer()
        {
            Console.WriteLine("Silmek istediğiniz müşterinin kullanıcı adını giriniz: ");
            string username = Console.ReadLine();

            string fileLocation = $@"C:\\Users\\Ensar's PC\\Desktop\\lab3dönem\\Yeni2\\Yeni2\\bin\\Debug\\MüsteriKayıtlar\\{username}.txt";

            if (File.Exists(fileLocation))
            {
                File.Delete(fileLocation);
                Console.WriteLine("İlgili kullanıcı programdan başarıyla silindi!");
            }
            else
            {
                Console.WriteLine("İlgili kullanıcı bulunamadı! Lütfen tekrar deneyiniz!\n");
            }
        }
        public void list_cart()
		{
			Console.WriteLine("Kullanıcı adınızı giriniz: ");
			string username = Console.ReadLine();

			string fileDestination = $@"C:\\Users\\Ensar's PC\\Desktop\\lab3dönem\\Yeni2\\Yeni2\\bin\\Debug\\MüsteriSepetBilgileri\\{username}SepetBilgi.txt";
			string[] bilgiler = File.ReadAllLines(fileDestination);

			foreach (string line in bilgiler)
			{
				Console.WriteLine(line);
			}
		}
	}
	class Admin : User
	{
		public void DeleteCustomer()
		{
			Console.WriteLine("Silmek istediğiniz müşterinin kullanıcı adını giriniz: ");
			string username = Console.ReadLine();

			string fileLocation = $@"C:\\Users\\Ensar's PC\\Desktop\\lab3dönem\\Yeni2\\Yeni2\\bin\\Debug\\MüsteriKayıtlar\\{username}.txt";
			
			if (File.Exists(fileLocation))
			{
				File.Delete(fileLocation);
				Console.WriteLine("İlgili kullanıcı programdan başarıyla silindi!");
			}
			else
			{
				Console.WriteLine("İlgili kullanıcı bulunamadı! Lütfen tekrar deneyiniz!\n");
			}
		}
		public void DeleteDealer()
		{
			again:
			Console.WriteLine("Silmek istediğiniz satıcının kullanıcı adını giriniz: ");
			string username = Console.ReadLine();

			string fileLocation = $@"C:\\Users\\Ensar's PC\\Desktop\\lab3dönem\\Yeni2\\Yeni2\\bin\\Debug\\SaticiKayitlar\\{username}.txt";

			if (File.Exists(fileLocation))
			{
				File.Delete(fileLocation);
                Console.WriteLine("İlgili kullanıcı programdan başarıyla silindi!");
            }
			else
			{
				Console.WriteLine("İlgili kullanıcı bulunamadı! Lütfen tekrar deneyiniz!");
				goto again;			
			}
		}
        public void DeleteAdmin()
        {
        again:
            Console.WriteLine("Silmek istediğiniz satıcının kullanıcı adını giriniz: ");
            string username = Console.ReadLine();

            string fileLocation = $@"C:\\Users\\Ensar's PC\\Desktop\\lab3dönem\\Yeni2\\Yeni2\\bin\\Debug\\SaticiKayitlar\\{username}.txt";

            if (File.Exists(fileLocation))
            {
                File.Delete(fileLocation);
                Console.WriteLine("İlgili kullanıcı programdan başarıyla silindi!");
            }
            else
            {
                Console.WriteLine("İlgili kullanıcı bulunamadı! Lütfen tekrar deneyiniz!");
                goto again;
            }
        }
		public void DeleteCar()
		{
			again:
			Console.WriteLine("Arabaların tüm özelliklerini silmek için \"1\" tuşlayınız.");
			Console.WriteLine("Sadece istediğiniz bir arabanın özelliğini silmek için \"2\" tuşlayınız.");
			string choose = Console.ReadLine();
			string fileLocation = $@"C:\\Users\\Ensar's PC\\Desktop\\lab3dönem\\Yeni2\\Yeni2\\bin\\Debug\\arabaBilgileri.txt";
			switch (choose)
			{
				case "1":
					Console.Clear();
                    if (File.Exists(fileLocation))
                    {
                        File.Delete(fileLocation);
                        Console.WriteLine("İlgili kullanıcı programdan başarıyla silindi!");
                    }
                    else
                    {
                        Console.WriteLine("İlgili kullanıcı bulunamadı! Lütfen tekrar deneyiniz!");
                        goto again;
                    }
				break; 
				case "2":
					Console.Clear();
                    Console.WriteLine("Silinecek Ürün Marka: ");
                    string DelBrand = Console.ReadLine();

                    Console.WriteLine("Silinecek Ürün Model: ");
                    string DelModel = Console.ReadLine();

                    Console.WriteLine("Silinecek Ürün Donanım Paketi: ");
                    string DelPacket = Console.ReadLine();

                    Console.WriteLine("Silinecek Ürün Yedek Parçası: ");
                    string DelSpare = Console.ReadLine();

                    List<string> UpdatedList = new List<string>();

                    using (StreamReader sr = new StreamReader(fileLocation))
                    {
                        string row;
                        while ((row = sr.ReadLine()) != null)
                        {
                            string[] CarListInfos = row.Split(',');

                            string brand = CarListInfos[0].Trim();
                            string model = CarListInfos[1].Trim();
                            string packet = CarListInfos[2].Trim();
                            string sparepart = CarListInfos[3].Trim();
                            string number = CarListInfos[4].Trim();

                            if (!(brand == DelBrand && model == DelModel && packet == DelPacket && sparepart == DelSpare))
                            {
                                UpdatedList.Add(row);
                            }
                        }
                    }
                    using (StreamWriter sw = new StreamWriter(fileLocation))
                    {
                        foreach (string RowCarInfos in UpdatedList)
                        {
                            sw.WriteLine(RowCarInfos);
                        }
                    }
                    Console.WriteLine("Ürün Başarıyla Silindi..");
                    break;
				default:
					Console.WriteLine("Hatalı bir tuşlama yaptınız! Lütfen tekrar deneyiniz!");
					goto again;
			}

		}
    }
	class Dealer : User
	{
		public void AddProduct()
		{
			Console.WriteLine("Marka Girin: ");
			string marka = Console.ReadLine();

			Console.WriteLine("Model Girin: ");
			string model = Console.ReadLine();

			Console.WriteLine("Donanım Paketi Girin: ");
			string donanımPaketi = Console.ReadLine();

			Console.WriteLine("Yedek Parçası Girin: ");
			string yedekparca = Console.ReadLine();

			Console.WriteLine("Yedek Parça Envanter Sayısı: ");
			string sayi = Console.ReadLine();

			string fileLocation = "arabaBilgileri.txt";

			using (StreamWriter sw = File.AppendText(fileLocation))
			{
				sw.WriteLine($"{marka},{model},{donanımPaketi},{yedekparca},{sayi}");
			}

			Console.WriteLine("Ürün başarıyla eklendi..");
		}
		public void ShowAllProducts()
		{
			string[] bilgiler = File.ReadAllLines("arabaBilgileri.txt");

			foreach (string line in bilgiler)
			{
				Console.WriteLine(line);
			}
		}
		public void DeleteProduct()
		{
			Console.WriteLine("Silinecek Ürün Marka: ");
			string DelBrand = Console.ReadLine();

			Console.WriteLine("Silinecek Ürün Model: ");
			string DelModel = Console.ReadLine();

			Console.WriteLine("Silinecek Ürün Donanım Paketi: ");
			string DelPacket = Console.ReadLine();

			Console.WriteLine("Silinecek Ürün Yedek Parçası: ");
			string DelSpare = Console.ReadLine();

			string fileLocation = "arabaBilgileri.txt";
			List<string> UpdatedList = new List<string>();

			using (StreamReader sr = new StreamReader(fileLocation))
			{
				string row;
				while ((row = sr.ReadLine()) != null)
				{
					string[] CarListInfos = row.Split(',');

					string brand = CarListInfos[0].Trim();
					string model = CarListInfos[1].Trim();
					string packet = CarListInfos[2].Trim();
					string sparepart = CarListInfos[3].Trim();
					string number = CarListInfos[4].Trim();

					if (!(brand == DelBrand && model == DelModel && packet == DelPacket && sparepart == DelSpare))
					{
						UpdatedList.Add(row);
					}
				}
			}
			using (StreamWriter sw = new StreamWriter(fileLocation))
			{
				foreach (string RowCarInfos in UpdatedList)
				{
					sw.WriteLine(RowCarInfos);
				}
			}
			Console.WriteLine("Ürün Başarıyla Silindi..");
		}
		public void UpdateSparePartsCount()
		{
			string[] infos = File.ReadAllLines("arabaBilgileri.txt");
			List<string> updatedInfos = new List<string>();

			Console.WriteLine("Marka Girin: ");
			string brand = Console.ReadLine();

			Console.WriteLine("Model Girin: ");
			string model = Console.ReadLine();

			Console.WriteLine("Donanım Paketi Girin: ");
			string packet = Console.ReadLine();

			Console.WriteLine("Yedek Parçası Girin: ");
			string sparepartsname = Console.ReadLine();

			Console.WriteLine("Yedek Parça Envanter Sayısı: ");
			string newNumber = Console.ReadLine();

			foreach (string line in infos)
			{
				string[] infoParts = line.Split(',');

				if (infoParts.Length > 0 && infoParts[0] == brand && infoParts[1] == model && infoParts[2] == packet && infoParts[3] == sparepartsname)
				{
					infoParts[4] = newNumber;
				}
				updatedInfos.Add(string.Join(",", infoParts));
			}

			File.WriteAllLines("arabaBilgileri.txt", updatedInfos);
			Console.WriteLine("Yedek Parça Sayısı Güncellendi!");
		}
		public bool CheckAnyOrder()
		{
			string folderLocation = @"C:\Users\Ensar's PC\Desktop\lab3dönem\Yeni2\Yeni2\bin\Debug\MüsteriSepetBilgileri";
			string[] files = Directory.GetFiles(folderLocation);
			int count = files.Length;

			if (files.Length > 0)
			{
				Console.WriteLine(count + " adet onaylanmayı bekleyen siparişiniz vardır!");
				foreach (string textNames in files)
				{
					string fileName = Path.GetFileName(textNames);
					Console.WriteLine("\nAraç yedek parçası satın alma talebi gönderen müşteri(nin\\lerin) kullanıcı adı: ");
					Console.WriteLine(fileName.Replace(".txt", ""));
				}
				return true;
			}
			else
			{
				Console.WriteLine("Onaylanmayı bekleyen siparişiniz bulunmamaktadır!");
			}
			return false;
		}
		public void ShowCustomerAccount()
		{
			Console.WriteLine("Görüntülemek istediğiniz kişinin kullanıcı adını giriniz: ");
			string username = Console.ReadLine();

			string fileLocation = "C:\\Users\\Ensar's PC\\Desktop\\lab3dönem\\Yeni2\\Yeni2\\bin\\Debug\\MüsteriKayıtlar";
			string filePath = Path.Combine(fileLocation, username + ".txt");

			if (File.Exists(filePath))
			{
				string fileContent = File.ReadAllText(filePath);
				Console.WriteLine($"\n{username} Kullanıcı Bilgileri: ");
				Console.WriteLine(fileContent);
			}
		}
		public void DecideAboutOrder()
		{
			if (CheckAnyOrder() == false)
			{
				return;
			}

			else
			{
				Console.WriteLine("Siparişine karar vereceğiniz kişinin kullanıcı adını giriniz: ");
				string username = Console.ReadLine();

				string SourceLocation = $@"C:\\Users\\Ensar's PC\\Desktop\\lab3dönem\\Yeni2\\Yeni2\\bin\\Debug\\MüsteriSepetBilgileri\\{username}SepetBilgi.txt";
				string DestinationLocation = $@"C:\\Users\\Ensar's PC\\Desktop\\lab3dönem\\Yeni2\\Yeni2\\bin\\Debug\\MüsteriKayıtlar\\{username}.txt";

				Console.WriteLine($"{username} kullanıcı adlı kişinin sipariş bilgileri: ");
				string[] bilgiler = File.ReadAllLines(SourceLocation);

				foreach (string line in bilgiler)
				{
					Console.WriteLine(line);
				}

				Console.WriteLine("Görüntülenen siparişi onaylamak istiyorsanız \"ONAYLA\" istemiyorsanız \"ONAYLAMA\" yazınız.");
				string choose = Console.ReadLine();

				List<string> SourceUpdatedList = new List<string>();
				List<string> DestinationUpdatedList = new List<string>();

				if (choose.ToLower() == "onayla")
				{
					string[] line = File.ReadAllLines(SourceLocation);

					using (StreamWriter sw = File.AppendText(DestinationLocation))
					{
						foreach (string row in line)
						{
							sw.WriteLine(row);
						}
					}
					Console.WriteLine("Müşterinin siparişine ONAY verdiniz!");
				}
				else if (choose.ToLower() == "onaylama")
				{
					if (File.Exists(SourceLocation))
					{
						File.Delete(SourceLocation);
						Console.WriteLine("Müşterinin siparişini RED ettiniz!");
					}
				}
			}
		}
	}
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Araç Yedek Parça Satış Yazılımı";

			Customer customer = new Customer();
			Dealer dealer = new Dealer();
			Admin admin = new Admin();

			bool donsunMu1 = true;
			while (donsunMu1)
			{
				mainMENU:
				Console.WriteLine("Müşteri panelini görüntülemek için (customer) yazın.");
				Console.WriteLine("Satıcı panelini görüntülemek için (dealer) yazın.");
				Console.WriteLine("Yönetici panelini görüntülemek için (admin) yazın.");

				string secim = Console.ReadLine();

				switch (secim.ToLower())
				{
					case "customer":
						CmainMENU1:
                        Console.Clear();
						Console.WriteLine("Müşteri paneline hoşgeldiniz!");
						Console.WriteLine("Müşteri üye girişi için \"1\" tuşlayınız.");
						Console.WriteLine("Müşteri kayıt olmak için \"2\" tuşlayınız.");
						Console.WriteLine("Kullanıcı panel menüsüne ulaşmak için \"3\" tuşlayınız.");
						
						string musteriSecim1 = Console.ReadLine();
						switch (musteriSecim1)
						{
							case "1":
								Console.Clear();
								CmainMENU2:
								if (customer.Login("customer") == 1)
								{
									CmainMENU3:
									Console.Clear();
									Console.WriteLine("Araç listesini, yedek parça ve envanter sayısını görüntülemek için \"1\" tuşlayınız.");
									Console.WriteLine("Yedek parça satın alma menüsüne erişmek için \"2\" tuşlayınız.");
									Console.WriteLine("Sepetinizdeki ürünleri listelemek için \"3\" tuşlayınız.");
									Console.WriteLine("Siparişinizin durumunu öğrenmek için \"4\" tuşlayınız.");
									Console.WriteLine("Müşteri hesap silme menüsü için \"5\" tuşlayınız.");
									Console.WriteLine("Müşteri giriş menüsüne ulaşmak için \"6\" tuşlayınız.");
									Console.WriteLine("Programdan ÇIKIŞ yapmak için \"7\" tuşlayınız.");
									string musteriSecim2 = Console.ReadLine();
										
									switch (musteriSecim2)
									{
										case "1":
											Console.Clear();
											customer.list_cars();
											CmainMENU4:
											Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
											string musteriSecim2_1 = Console.ReadLine();
											
											if (musteriSecim2_1 ==  "1")
											{
												goto CmainMENU3;
											}
											else
											{
												Console.WriteLine("Yanlış bir ifade tuşladınız!");
												goto CmainMENU4;
											}
										case "2":
											Console.Clear();
											Console.WriteLine("Araç listesini, yedek parça ve envanter sayısını görüntülemek için \"1\" tuşlayınız.");
											Console.WriteLine("Listelemeden devam etmek istiyorsanız \"2\" tuşlayınız.");
											string musteriSecim3 = Console.ReadLine();
											if (musteriSecim3 == "1")
											{
												Console.Clear();
												customer.ShowAllProducts();
												Console.WriteLine();
												customer.AddCart();
												CmainMENU5:
                                                Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
                                                Console.WriteLine("Sepetinizdeki ürünleri listelemek için \"2\" tuşlayınız.");
                                                string musteriSecim3_1 = Console.ReadLine();

                                                if (musteriSecim3_1 == "1")
                                                {
                                                    goto CmainMENU3;
                                                }
												else if (musteriSecim3_1 == "2")
												{
													Console.Clear();
													goto case "3";
												}
                                                else
                                                {
                                                    Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                    goto CmainMENU5;
                                                }
                                            }
											else if (musteriSecim3 == "2")
											{
												Console.Clear();
												customer.AddCart();
												CmainMENU6:
                                                Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
                                                Console.WriteLine("Sepetinizdeki ürünleri listelemek için \"2\" tuşlayınız.");
                                                string musteriSecim3_3e = Console.ReadLine();

                                                if (musteriSecim3_3e == "1")
                                                {
                                                    goto CmainMENU3;
                                                }
                                                else if (musteriSecim3_3e == "2")
                                                {
                                                    Console.Clear();
                                                    goto case "3";
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                    goto CmainMENU6;
                                                }
                                            }
											else
											{
											Console.WriteLine("Yanlış bir ifade tuşladınız!");
											goto case "2";
											}
										case "3":
											Console.Clear();
											Console.WriteLine("Sepetinizdeki ürünler şu şekildedir: ");
											customer.list_cart();
											CmainMENU7:
                                            Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
                                            string musteriSecim3_3 = Console.ReadLine();

                                            if (musteriSecim3_3 == "1")
                                            {
                                                goto CmainMENU3;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                goto CmainMENU7;
                                            }
										case "4":
											Console.Clear();
                                            customer.ShowApprovedProducts();
											CmainMENU8:
                                            Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
                                            string musteriSecim3_4 = Console.ReadLine();

                                            if (musteriSecim3_4 == "1")
                                            {
                                                goto CmainMENU3;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                goto CmainMENU8;
                                            }
										case "5":
											CmainMENUretry:
											Console.Clear();
											customer.DeleteCustomer();
											CmainMENU9:
                                            Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
                                            Console.WriteLine("Silme işlemini tekrar yapmak için \"2\" tuşlayınız.");
                                            string musteriSecim3_5 = Console.ReadLine();

                                            if (musteriSecim3_5 == "1")
                                            {
                                                goto CmainMENU3;
                                            }
											else if (musteriSecim3_5 == "2")
											{
												goto CmainMENUretry;
											}
                                            else
                                            {
                                                Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                goto CmainMENU9;
                                            }
                                        case "6":
											Console.Clear();
											goto CmainMENU1;
										case "7":
											goto cikis;
										default:
											Console.Clear();
											Console.WriteLine("Hatalı tuşlama yaptınız!");
											break;
									}
								}
								else
								{
									goto CmainMENU2;
								}
							break;
							case "2":
								Console.Clear();
								customer.Register("customer");
								Console.WriteLine("Müşteri paneline yönlendiriliyorsunuz!");
								goto CmainMENU1;
							case "3":
								Console.Clear();
								goto mainMENU;
							default:
								Console.WriteLine("Hatalı bir tuşlama yaptınız!");
								goto CmainMENU1;
						}
						break;
					case "dealer":
						DmainMENU:
                        Console.Clear();
						Console.WriteLine("Satıcı paneline hoşgeldiniz!");
						Console.WriteLine("Satıcı Üye girişi için \"1\" tuşlayınız.");
						Console.WriteLine("Satıcı Kayıt olmak için \"2\" tuşlayınız.");
                        Console.WriteLine("Kullanıcı panel menüsüne ulaşmak için \"3\" tuşlayınız.");

                        string dealerChoose1 = Console.ReadLine();

						switch (dealerChoose1)
						{
							case "1":
								Console.Clear();
								if (dealer.Login("dealer") == 1)
								{
									DmainMENU1:
									Console.Clear();
									Console.WriteLine("Mevcut araçların listesini, yedek parça ve envanter sayısını görüntülemek için \"1\" tuşlayınız.");
									Console.WriteLine("Mevcut araçlara ait yedek parça sayısı bilgisini (yedek parça sayacını) güncellemek için \"2\" tuşlayınız. ");
									Console.WriteLine("Mevcut araçlara yeni bir araç eklemek için \"3\" tuşlayınız.");
									Console.WriteLine("Mevcut araçlara ait bilgileri ve özellikleri silmek için \"4\" tuşlayınız");
									Console.WriteLine("Sipariş durumu kontrolü ve müşteri hesap bilgilerini görüntülemek için \"5\" tuşlayınız.");
									Console.WriteLine("Varolan siparişe ait onay menüsü için \"6\" tuşlayınız.");
									Console.WriteLine("Hesap silme menüsü için \"7\" tuşlayınız.");
                                    Console.WriteLine("Satıcı giriş menüsüne ulaşmak için \"8\" tuşlayınız.");
                                    Console.WriteLine("Programdan ÇIKIŞ yapmak için \"9\" tuşlayınız.");

                                    string dealerChoose2 = Console.ReadLine();
									switch (dealerChoose2)
									{
										case "1":
											Console.Clear();
											Console.WriteLine("Mevcut Araç Liste Bilgileri: ");
											dealer.ShowAllProducts();
											
											DmainMENU2:
                                            Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
                                            string dealerSecim2_1 = Console.ReadLine();

                                            if (dealerSecim2_1 == "1")
                                            {
                                                goto DmainMENU1;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                goto DmainMENU2;
                                            }
										case "2":
											Console.Clear();
											Console.WriteLine("Güncelleme yapmadan önce araçların listesini, yedek parça ve envanter sayısını görüntülemek için \"1\" tuşlayınız.");
											Console.WriteLine("Listelemeden devam etmek istiyorsanız \"2\" tuşlayınız.");
											string dealerChoose3 = Console.ReadLine();
											if (dealerChoose3 == "1")
											{
												dealer.ShowAllProducts();
												Console.WriteLine("\nGüncellenecek araca ait bilgileri giriniz: ");
												dealer.UpdateSparePartsCount();
                                                
												DmainMENU3:
                                                Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
                                                string dealerSecim3_1 = Console.ReadLine();

                                                if (dealerSecim3_1 == "1")
                                                {
                                                    goto DmainMENU1;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                    goto DmainMENU3;
                                                }
                                            }
											else if (dealerChoose3 == "2")
											{
												Console.WriteLine("\nGüncellenecek araca ait bilgileri giriniz: ");
												dealer.UpdateSparePartsCount();
												
												DmainMENU4:
                                                Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
                                                string dealerSecim3_2 = Console.ReadLine();

                                                if (dealerSecim3_2 == "1")
                                                {
                                                    goto DmainMENU1;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                    goto DmainMENU4;
                                                }
                                            }
											else
											{
												Console.WriteLine("Yanlış bir ifade tuşladınız!");
												goto case "2";
											}
										case "3":
											Console.Clear();
											Console.WriteLine("\nEklenecek araca ait bilgileri giriniz: ");
											dealer.AddProduct();

											DmainMENU5:
                                            Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
                                            string dealerSecim_e1 = Console.ReadLine();

                                            if (dealerSecim_e1 == "1")
                                            {
                                                goto DmainMENU1;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                goto DmainMENU5;
                                            }
										case "4":
											Console.Clear();
											Console.WriteLine("Silme işlemi yapmadan önce araçların listesini, yedek parça ve envanter sayısını görüntülemek için \"1\" tuşlayınız.");
											Console.WriteLine("Listelemeden devam etmek istiyorsanız \"2\" tuşlayınız.");
											string dealerChoose4 = Console.ReadLine();

											if (dealerChoose4 == "1")
											{
												dealer.ShowAllProducts();
												Console.WriteLine("\nSilinecek araca ait bilgileri giriniz: ");
												dealer.DeleteProduct();

												DmainMENU6:
                                                Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
                                                string dealerSecim4_1 = Console.ReadLine();

                                                if (dealerSecim4_1 == "1")
                                                {
                                                    goto DmainMENU1;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                    goto DmainMENU6;
                                                }
                                            }
											else if (dealerChoose4 == "2")
											{
												Console.WriteLine("\nSilinecek araca ait bilgileri giriniz: ");
												dealer.DeleteProduct();
												
												DmainMENU7:
                                                Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
                                                string dealerSecim4_2 = Console.ReadLine();

                                                if (dealerSecim4_2 == "1")
                                                {
                                                    goto DmainMENU1;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                    goto DmainMENU7;
                                                }
                                            }
											else
											{
												Console.WriteLine("Yanlış bir ifade tuşladınız!");
												goto case "4";
											}
										case "5":
											Console.Clear();
											Console.WriteLine("Sipariş Durumu Kontrol Menüsü: ");
											if (dealer.CheckAnyOrder() == true)
											{
												Console.WriteLine("\nSipariş gönderen kullanıcıların hesap bilgilerini görüntülemek için \"1\" tuşlayınız.");
												string dealerChoose5 = Console.ReadLine();
												if (dealerChoose5 == "1")
												{
													dealer.ShowCustomerAccount();
													
													DmainMENU8:
                                                    Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
                                                    string dealerSecim5_1 = Console.ReadLine();

                                                    if (dealerSecim5_1 == "1")
                                                    {
                                                        goto DmainMENU1;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                        goto DmainMENU8;
                                                    }
                                                }
											}
											else
											{
												dealer.CheckAnyOrder();
												
												DmainMENU9:
                                                Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
                                                string dealerSecim5_2 = Console.ReadLine();

                                                if (dealerSecim5_2 == "1")
                                                {
                                                    goto DmainMENU1;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                    goto DmainMENU9;
                                                }
                                            }
											break;
										case "6":
											Console.Clear();
											dealer.DecideAboutOrder();
											
											DmainMENU10:
                                            Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
                                            string dealerSecim6_1 = Console.ReadLine();

                                            if (dealerSecim6_1 == "1")
                                            {
                                                goto DmainMENU1;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                goto DmainMENU10;
                                            }
                                        case "7":
											DmainMENU11:
                                            Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
                                            string dealerSecim7_1 = Console.ReadLine();

                                            if (dealerSecim7_1 == "1")
                                            {
                                                goto DmainMENU1;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                goto DmainMENU11;
                                            }
										case "8":
											goto DmainMENU;
										case "9":
											goto cikis;
									}
								}
								break;
							case "2":
								Console.Clear();
								dealer.Register("dealer");
                                
								Console.WriteLine("Satıcı paneline yönlendiriliyorsunuz!");
                                goto DmainMENU;
							case "3":
                                Console.Clear();
                                goto mainMENU;
                        }
						break;
					case "admin":
						AmainMENU1:
                        Console.Clear();
                        Console.WriteLine("Yönetici paneline hoşgeldiniz!");
                        Console.WriteLine("Yönetici üye girişi için \"1\" tuşlayınız.");
                        Console.WriteLine("Kullanıcı panel menüsüne ulaşmak için \"2\" tuşlayınız.");

                        string adminSecim1 = Console.ReadLine();
						switch (adminSecim1)
						{
							case "1":
                                Console.Clear();
								AmainMENU2:
								if (admin.Login("admin") == 1)
								{
									AmainMENU3:
                                    Console.Clear();
									Console.WriteLine("Müşteri hesap silme menüsüne ulaşmak için \"1\" tuşlayınız.");
									Console.WriteLine("Satıcı hesap silme menüsüne ulaşmak için \"2\" tuşlayınız.");
									Console.WriteLine("Yönetici hesap silme menüsüne ulaşmak için \"3\" tuşlayınız.");
									Console.WriteLine("Araba özellik silme menüsüne ulaşmak için \"4\" tuşlayınız.");
									Console.WriteLine("Yönetici giriş menüsüne ulaşmak için \"5\" tuşlayınız.");
                                    Console.WriteLine("Programdan ÇIKIŞ yapmak için \"6\" tuşlayınız.");
                                    string adminSecim2 = Console.ReadLine();
									switch (adminSecim2)
									{
										case "1":
											AmainMENUretry1:
                                            Console.Clear();											
											admin.DeleteCustomer();
											AmainMENU4:
                                            Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
											Console.WriteLine("Silme işlemini tekrar yapmak için \"2\" tuşlayınız.");
                                            string adminSecim2_1 = Console.ReadLine();

                                            if (adminSecim2_1 == "1")
                                            {
                                                goto AmainMENU3;
                                            }
                                            else if (adminSecim2_1 == "2")
                                            {
												goto AmainMENUretry1;
                                            }
											else
											{
                                                Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                goto AmainMENU4;
                                            }
										case "2":
											AmainMENUretry2:
                                            Console.Clear();											
                                            admin.DeleteDealer();
											AmainMENU5:
                                            Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
                                            Console.WriteLine("Silme işlemini tekrar yapmak için \"2\" tuşlayınız.");
                                            string musteriSecim2_2 = Console.ReadLine();

                                            if (musteriSecim2_2 == "1")
                                            {
                                                goto AmainMENU3;
                                            }
											else if (musteriSecim2_2 == "2")
											{
												goto AmainMENUretry2;
											}
                                            else
                                            {
                                                Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                goto AmainMENU5;
                                            }
										case "3":
											AmainMENUretry3:
											Console.Clear();
											admin.DeleteAdmin();
											AmainMENU6:
                                            Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
                                            Console.WriteLine("Silme işlemini tekrar yapmak için \"2\" tuşlayınız.");
                                            string adminSecim2_3 = Console.ReadLine();

											if (adminSecim2_3 == "1")
											{
												goto AmainMENU3;
											}
											else if (adminSecim2_3 == "2")
											{
												goto AmainMENUretry3;
											}
                                            else
                                            {
                                                Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                goto AmainMENU6;
                                            }
										case "4":
											AmainMENUretry4:
											Console.Clear();
											admin.DeleteCar();
											AmainMENU7:
                                            Console.WriteLine("\nAna menüye geçmek için \"1\" tuşlayınız.");
                                            Console.WriteLine("Silme işlemini tekrar yapmak için \"2\" tuşlayınız.");
                                            string adminSecim2_4 = Console.ReadLine();

                                            if (adminSecim2_4 == "1")
                                            {
                                                goto AmainMENU3;
                                            }
											else if (adminSecim2_4 == "2")
											{
												goto AmainMENUretry4;
											}
                                            else
                                            {
                                                Console.WriteLine("Yanlış bir ifade tuşladınız!");
                                                goto AmainMENU7;
                                            }
										case "5":
											goto AmainMENU1;
										case "6":
											goto cikis;											
                                    }
                                }
								else
								{
									goto AmainMENU2;
								}
								break;
							case "2":
                                Console.Clear();
                                goto mainMENU;
							default:
                                Console.WriteLine("Hatalı bir tuşlama yaptınız!");
                                goto AmainMENU1;
                        }
                        break;

					default:
						Console.WriteLine("Hatalı bir seçenek yazdınız. Tekrar giriş yapınız!\n");
						break;
				}
				
				cikis:
				Console.Write("Programdan Çıkılıyor");
				for (int i = 0; i < 3; i++) 
				{
					Console.Write(".");
				}
                System.Threading.Thread.Sleep(700);
				donsunMu1 = false;
            }

		}
	}
	
}