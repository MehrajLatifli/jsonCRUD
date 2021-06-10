using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace jsonCRUD
{

    class User
    {
        public string Name { get; set; }
        public string SurName { get; set; }

    }

    class Inventory
    {
        public string inventoryName  { get; set; }
        public string inventoryVendor { get; set; }
    }

    class Rootobject
    {
        public List<User> Users { get; set; }
        public List<Inventory> Inventories { get; set; }

        public Rootobject()
        {
            Users = new List<User>();
            Inventories = new List<Inventory>();
        }

    }



    class Program
    {
        public static string FilePath { get; set; } = "../../../File.json";
        private static Rootobject RootList;
        static void Main(string[] args)
        {
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath).Close();
            }
            RootList = DeserializeFile();




            while (true)
            {
                Console.WriteLine("\n 1 <- AddUser \n 2 <- EditUser \n 3 <- DeleteUser \n 4 <- UpdateUserList \n\n 5 <- AddInventory \n 6 <- EditInventory \n 7 <- DeleteInventory \n 8 <- UpdateInventory ");
                Console.Write("\n\n Enter: ");
                int select = Convert.ToInt32(Console.ReadLine());

                switch (select)
                {
                    case 1:
                        AddUser();
                        break;
                    case 2:
                        EditUser();
                        break;
                    case 3:
                        DeleteUser();
                        break;
                    case 4:
                        UpdateUserList();
                        break;
                    case 5:
                        AddInventory();
                        break;
                    case 6:
                        EditInventory();
                        break;
                    case 7:
                        DeleteInventory();
                        break;
                    case 8:
                        UpdateInventory();
                        break;
                    default:
                        break;
                }

            }

            Console.ReadKey();

        }

        private static void AddInventory()
        {
            RootList = DeserializeFile();


            Console.Write("\n Enter Inventoryname: ");
            string InventoryName = Console.ReadLine();
            Console.Write("\n Enter InventoryVendor: ");
            string InventoryVendor = Console.ReadLine();

            Inventory inventory = new Inventory
            {
                 inventoryName = InventoryName,
                 inventoryVendor = InventoryVendor
            };




            RootList.Inventories.Add(inventory);

            SerializeList(RootList);
        }

        private static void EditInventory()
        {
            UpdateInventory();

            Console.Write("\n Enter index for edit: ");

            int index = int.Parse(Console.ReadLine());

            Console.Write("\n Enter Inventoryname: ");
            string InventoryName = Console.ReadLine();
            Console.Write("\n Enter InventoryVendor: ");
            string InventoryVendor = Console.ReadLine();

            RootList.Inventories[index].inventoryName = InventoryName;
            RootList.Inventories[index].inventoryVendor = InventoryVendor;

            SerializeList(RootList);
        }

        private static void DeleteInventory()
        {

            UpdateInventory();

            Console.Write("\n Enter index for delete: ");

            int index = int.Parse(Console.ReadLine());

            RootList.Inventories.RemoveAt(index);

            SerializeList(RootList);
        }


        private static void UpdateInventory()
        {
            RootList = DeserializeFile();

            foreach (var item in RootList.Inventories)
            {
                Console.WriteLine($"\n {item.inventoryName} {item.inventoryVendor}");
            }
        }

  


        private static void AddUser()
        {
            RootList = DeserializeFile();


            Console.Write("\n Enter name: ");
            string name = Console.ReadLine();
            Console.Write("\n Enter surname: ");
            string surname = Console.ReadLine();

            User user = new User()
            {
                Name = name,
                SurName = surname
            };




            RootList.Users.Add(user);

            SerializeList(RootList);

        }
        private static void EditUser()
        {
            UpdateUserList();

            Console.Write("\n Enter index for edit: ");

            int index = int.Parse(Console.ReadLine());

            Console.Write("\n Enter name: ");
            string name = Console.ReadLine();
            Console.Write("\n Enter surname: ");
            string surname = Console.ReadLine();

            RootList.Users[index].Name = name;
            RootList.Users[index].SurName = surname;

            SerializeList(RootList);
        }
        private static void DeleteUser()
        {
            UpdateUserList();

            Console.Write("\n Enter index for delete: ");

            int index = int.Parse(Console.ReadLine());

            RootList.Users.RemoveAt(index);

            SerializeList(RootList);
        }
        private static void UpdateUserList()
        {
            RootList = DeserializeFile();

            foreach (var item in RootList.Users)
            {
                Console.WriteLine($"\n {item.Name} {item.SurName}");
            }
        }




        private static Rootobject DeserializeFile()
        {


            string data = File.ReadAllText(FilePath);

            RootList = JsonConvert.DeserializeObject<Rootobject>(data);

            if (RootList != null)
            {
                return RootList;
            }



            return new Rootobject();
        }

        private static void SerializeList(Rootobject users)
        {
            var f = Newtonsoft.Json.Formatting.Indented;
            string data = JsonConvert.SerializeObject(users, f);

            File.WriteAllText(FilePath, data);
        }
    }
}
