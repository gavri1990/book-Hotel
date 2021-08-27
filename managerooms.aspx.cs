using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

namespace Hotel_Booking_App
{
    public partial class WebForm11 : System.Web.UI.Page
    {
        Model1Container1 context = new Model1Container1();
        int roomId; //global, θα της καταχωρίσουμε αργότερα το r.Id του item
        static string roomToUpdateNumber = ""; //static μεταβλητή, χρήση για εντοπισμού room που θα γίνει update

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)  //αν ερχόμαστε 1η φορά στο page, όχι με refresh μετά το πάτημα UPDATE button
            {
                Label1.Visible = false;
                TextBox1.Visible = false;
                TextBox10.Visible = false;
                TextBox11.Visible = false;
                DropDownList3.Visible = false;
                FileUpload1.Visible = false;
                Label3.Visible = false;
                Label4.Visible = false;
                CheckBox1.Visible = false;
                CheckBox2.Visible = false;
                CheckBox3.Visible = false;
                CheckBox4.Visible = false;
                CheckBox5.Visible = false;
                Button2.Visible = false;
                Button3.Visible = false;
                Button5.Visible = false;
                Label5.Visible = false;
                Button6.Visible = false;
                Button7.Visible = false;
            }
        }


        protected void Button1_Click(object sender, EventArgs e)  //search room button
        {
            try
            {
                string email = Session["email"].ToString();
                string pass = Session["pass"].ToString();
                string maxpers;

                var query = from ho in context.HotelOwnerSet
                            join h in context.HotelSet on ho.Id equals h.HotelOwner.Id
                            join r in context.RoomSet on h.Id equals r.Hotel.Id
                            where ((ho.Email.Equals(email)) && (ho.Password.Equals(pass)) && (r.RoomNumber.Equals(TextBox9.Text)))
                            select new { r.Id, r.RoomNumber, r.RoomName, r.Rate, r.MaxPersons };

                if (query.Any())
                {
                    roomToUpdateNumber = TextBox9.Text; //αποθήκευση δωματίου που θα γίνει update στη static μεταβλητή
                    Label1.Text = "Room entry found!";
                    Label1.ForeColor = Color.Green;
                    Label1.Visible = true;
                    TextBox1.Visible = true;
                    TextBox10.Visible = true;
                    TextBox11.Visible = true;
                    DropDownList3.Visible = true;
                    FileUpload1.Visible = true;
                    Label3.Visible = true;
                    Label3.Text = "Make any necessary changes or delete current room entry below";
                    Label4.Visible = true;
                    CheckBox1.Visible = true;
                    CheckBox2.Visible = true;
                    CheckBox3.Visible = true;
                    CheckBox4.Visible = true;
                    CheckBox5.Visible = true;
                    Button2.Visible = true;
                    Button3.Visible = true;
                    Button5.Visible = false;
                    Label5.Visible = false;
                    Button6.Visible = false;
                    Button7.Visible = false;
                    FileUpload1.Focus();


                    foreach (var item in query)
                    {
                        roomId = item.Id; //καταχώριση r.Id σε roomId ώστε να είναι ορατό και στον κώδικα του επόμενου button 
                        Session["roomId"] = roomId; //περνάμε στο Session για να είναι ορατό στις άλλες methods της WebForm

                        TextBox1.Text = item.RoomNumber.ToString();
                        TextBox10.Text = item.RoomName;
                        TextBox11.Text = item.Rate.ToString().Replace(",", ".");
                        maxpers = item.MaxPersons.ToString();
                        DropDownList3.SelectedIndex = DropDownList3.Items.IndexOf(DropDownList3.Items.FindByValue("max " + maxpers + " persons"));

                        var query1 = from s in context.ServiceSet
                                     where s.Room.Id == item.Id
                                     select new { s.ServiceName };

                        CheckBox1.Checked = false; //ξετικάρουμε όλα τα Checkboxes, πιθανώς να έχουν τικαριστεί απο προηγούμενο room
                        CheckBox2.Checked = false;
                        CheckBox3.Checked = false;
                        CheckBox4.Checked = false;
                        CheckBox5.Checked = false;


                        if (query1.Any()) //αν βρεθούν services σχετιζόμενα με το συγκεκριμένο room
                        {
                            foreach (var item1 in query1) //σύγκριση κάθε service name του query με το text κάθε checkbox
                            {
                                if (item1.ServiceName.Equals(CheckBox1.Text))
                                {
                                    CheckBox1.Checked = true; //αν είναι ίδια, τικάρεται το checkbox
                                }
                                else if (item1.ServiceName.Equals(CheckBox2.Text))
                                {
                                    CheckBox2.Checked = true;
                                }
                                else if (item1.ServiceName.Equals(CheckBox3.Text))
                                {
                                    CheckBox3.Checked = true;
                                }
                                else if (item1.ServiceName.Equals(CheckBox4.Text))
                                {
                                    CheckBox4.Checked = true;
                                }
                                else if (item1.ServiceName.Equals(CheckBox5.Text))
                                {
                                    CheckBox5.Checked = true;
                                }

                            }
                        }
                    }

                }
                else
                {
                    Label1.Text = "No room entry found with this number";
                    Label1.ForeColor = Color.Red;
                    Label1.Visible = true;
                    TextBox1.Visible = false;
                    TextBox10.Visible = false;
                    TextBox11.Visible = false;
                    DropDownList3.Visible = false;
                    FileUpload1.Visible = false;
                    Label3.Visible = false;
                    Label4.Visible = false;
                    CheckBox1.Visible = false;
                    CheckBox2.Visible = false;
                    CheckBox3.Visible = false;
                    CheckBox4.Visible = false;
                    CheckBox5.Visible = false;
                    Button2.Visible = false;
                    Button3.Visible = false;
                    Button5.Visible = false;
                    Label5.Visible = false;
                    Button6.Visible = false;
                    Button7.Visible = false;
                }
            }
            catch (Exception exc)
            {
                Label1.Text = "Something went wrong! Please try reloading the page";
                Label1.ForeColor = Color.Red;
                Label1.Visible = true;
                TextBox1.Visible = false;
                TextBox10.Visible = false;
                TextBox11.Visible = false;
                DropDownList3.Visible = false;
                FileUpload1.Visible = false;
                Label3.Visible = false;
                Label4.Visible = false;
                CheckBox1.Visible = false;
                CheckBox2.Visible = false;
                CheckBox3.Visible = false;
                CheckBox4.Visible = false;
                CheckBox5.Visible = false;
                Button2.Visible = false;
                Button3.Visible = false;
                Button5.Visible = false;
                Label5.Visible = false;
                Button6.Visible = false;
                Button7.Visible = false;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)  //update room button
        {
            if (string.IsNullOrWhiteSpace(TextBox1.Text) || string.IsNullOrWhiteSpace(TextBox10.Text) || string.IsNullOrWhiteSpace(TextBox11.Text))
            {
                Label1.Text = "Please fill all required fields";
                Label1.ForeColor = Color.Red;
                Label1.Visible = true;
                TextBox1.Visible = true;
                TextBox10.Visible = true;
                TextBox11.Visible = true;
                DropDownList3.Visible = true;
                FileUpload1.Visible = true;
                Label3.Visible = true;
                Label3.Text = "Make any necessary changes or delete current room entry below";
                Label4.Visible = true;
                CheckBox1.Visible = true;
                CheckBox2.Visible = true;
                CheckBox3.Visible = true;
                CheckBox4.Visible = true;
                CheckBox5.Visible = true;
                Button2.Visible = true;
                Button3.Visible = true;
                Button5.Visible = false;
                Label5.Visible = false;
                Button6.Visible = false;
                Button7.Visible = false;
                FileUpload1.Focus();
            }
            else
            {
                roomId = Convert.ToInt16(Session["roomId"]); //παίρνουμε το roomId από το Session, μπήκε εκεί στην πιο πάνω method
                string hotelOwnerEmail = (string)Session["email"]; //παίρνουμε το HotelOwnerEmail από το Session



                try
                {
                    if (!TextBox1.Text.Equals(roomToUpdateNumber)) //αν άλλαξε νούμερο room που θέλει να κάνει update
                    {
                        var query0 = from ho in context.HotelOwnerSet //query για εντοπισμό ήδη υπαρχόντων δωματίων με ίδιο όνομα
                                     join h in context.HotelSet on ho.Id equals h.HotelOwner.Id
                                     join r in context.RoomSet on h.Id equals r.Hotel.Id
                                     where ((ho.Email.Equals(hotelOwnerEmail)) && (r.RoomNumber.Equals(TextBox1.Text)))
                                     select new { r.Id };


                        if (query0.Any()) //αν υπάρχει ήδη δωμάτιο του hotel owner με το νούμερο που εισήχθη
                        {
                            Label1.Text = "Room '" + TextBox1.Text + "' is already registered";
                            Label1.ForeColor = Color.Red;
                            Label1.Visible = true;
                            Label3.Visible = true;
                            Label3.Text = "Please choose a different room number";
                            Button4.Focus();
                            return; //έξοδος από τη method
                        }
                        else //αν το επιλεγμένο room number είναι διαθέσιμο
                        {
                            //αντικατάσταση ονόματος φακέλλου με φωτό του room με βάση το νέο επιλεγμένο room number
                            Directory.Move(Server.MapPath("~/HotelRoomImages//" + hotelOwnerEmail + "//" + roomToUpdateNumber), Server.MapPath("~/HotelRoomImages//" + hotelOwnerEmail + "//" + TextBox1.Text));
                        }
                    }



                    var query1 =
                        from room in context.RoomSet //query για εύρεση των πεδίων που αφορούν στο room
                        where room.Id == roomId
                        select room;

                    var query2 =
                            from service in context.ServiceSet //query για εύρεση των πεδίων που αφορούν στα services του room(ήδη συνδεδεμένα με αυτό)
                            where service.Room.Id == roomId
                            select service;


                    foreach (Room r in query1) //Room r αντί για var item, update πεδίων room
                    {
                        r.RoomName = TextBox10.Text;
                        r.Rate = Convert.ToDouble(TextBox11.Text, System.Globalization.CultureInfo.InvariantCulture);
                        //1 ενδιάμεσος χαρακτήρας από DropdownList, θέλουμε τον αριθμό μεταξύ των λέξεων max και persons
                        string selectedMaxPersons = DropDownList3.SelectedValue.Substring(4, 1);
                        r.MaxPersons = Convert.ToInt16(selectedMaxPersons);
                        r.RoomNumber = TextBox1.Text;
                        if (FileUpload1.HasFile) //αν έχει επιλέξει αρχείο ο Hotel Owner
                        {
                            if (!string.IsNullOrEmpty(r.RoomPhotoUrl)) //αν έχει ήδη αποθηκευτεί φωτό σε project
                            {
                                System.IO.File.Delete(Server.MapPath("~/HotelRoomImages//" + hotelOwnerEmail + "//" + TextBox1.Text + "//" + r.RoomPhotoUrl)); //διαγραφή από τον φάκελο του project της προηγούμενης αποθηκευμένης φωτό room
                            }

                            r.RoomPhotoUrl = FileUpload1.FileName; //αποθήκευση αρχείου που ανεβάστηκε στην στήλη του RoomSet
                            FileUpload1.SaveAs(Server.MapPath("~/HotelRoomImages//" + hotelOwnerEmail + "//" + TextBox1.Text + "//" + FileUpload1.FileName)); //αποθήκευση και στον φάκελο του project      
                        }
                    }

                    List<string> checkedServices = new List<string>(); //δημιουργία list τύπου string
                    List<string> DbServices = new List<string>(); //δημιουργία list τύπου string

                    if (CheckBox1.Checked == true) //αν το checkBox είναι checked, πρόσθεση Text του σε List
                    {
                        checkedServices.Add(CheckBox1.Text);
                    }
                    if (CheckBox2.Checked == true)
                    {
                        checkedServices.Add(CheckBox2.Text);
                    }
                    if (CheckBox3.Checked == true)
                    {
                        checkedServices.Add(CheckBox3.Text);
                    }
                    if (CheckBox4.Checked == true)
                    {
                        checkedServices.Add(CheckBox4.Text);
                    }
                    if (CheckBox5.Checked == true)
                    {
                        checkedServices.Add(CheckBox5.Text);
                    }

                    foreach (Service serv in query2)  //delete services που έχουν αποεπιλεχθεί στο αντίστοιχο CheckBox
                    {
                        DbServices.Add(serv.ServiceName); //παράλληλα προσθήκη όλων των services της ΒΔ σε list, χρήση μετά
                        if (!checkedServices.Contains(serv.ServiceName)) //αν δεν περιλαμβάνεται σε list (ενώ υπάρχει σε ΒΔ)
                        {
                            context.ServiceSet.Remove(serv); //διαγραφή Service από ΒΔ
                        }
                    }


                    for (int i = 0; i < checkedServices.Count; i++)//για κάθε service που έχει γίνει checked στο CheckBox
                    {
                        if (!DbServices.Contains(checkedServices[i]))
                        {
                            Service s1 = new Service();
                            s1.ServiceName = checkedServices[i]; //προσθήκη service από checked CheckBox σε ΒΔ
                            foreach (Room r in query1)
                            {
                                r.Service.Add(s1);  //σύνδεση με το υπό επεξεργασία room
                            }
                        }
                    }
                    context.SaveChanges(); //αποθηκευση αλλαγών στη ΒΔ
                    Button2.Focus(); //focus δεν ανεβαίνει στην κορυφή σελίδας

                    Label1.Text = "Room entry updated successfully!";
                    Label1.ForeColor = Color.Green;
                    Label1.Visible = true;
                    TextBox9.Text = ""; //καθαρισμός TextBox για search νέου room
                    TextBox1.Visible = false;
                    TextBox10.Visible = false;
                    TextBox11.Visible = false;
                    DropDownList3.Visible = false;
                    FileUpload1.Visible = false;
                    Label3.Visible = false;
                    Label4.Visible = false;
                    CheckBox1.Visible = false;
                    CheckBox2.Visible = false;
                    CheckBox3.Visible = false;
                    CheckBox4.Visible = false;
                    CheckBox5.Visible = false;
                    Button2.Visible = false;
                    Button3.Visible = false;
                    Button5.Visible = false;
                    Label5.Visible = false;
                    Button6.Visible = false;
                    Button7.Visible = false;
                }
                catch (Exception exc)
                {
                    Label1.Text = "Something went wrong! Please try reloading the page";
                    Label1.ForeColor = Color.Red;
                    Label1.Visible = true;
                    TextBox1.Visible = false;
                    TextBox10.Visible = false;
                    TextBox11.Visible = false;
                    DropDownList3.Visible = false;
                    FileUpload1.Visible = false;
                    Label3.Visible = false;
                    Label4.Visible = false;
                    CheckBox1.Visible = false;
                    CheckBox2.Visible = false;
                    CheckBox3.Visible = false;
                    CheckBox4.Visible = false;
                    CheckBox5.Visible = false;
                    Button2.Visible = false;
                    Button3.Visible = false;
                    Button5.Visible = false;
                    Label5.Visible = false;
                    Button6.Visible = false;
                    Button7.Visible = false;
                }
            }
        }

        protected void Button3_Click(object sender, EventArgs e)  //delete room button
        {
            Label1.Text = "Room entry found!";
            Label1.ForeColor = Color.Green;
            Label1.Visible = true;
            TextBox1.Visible = true;
            TextBox10.Visible = true;
            TextBox11.Visible = true;
            DropDownList3.Visible = true;
            FileUpload1.Visible = true;
            Label3.Visible = true;
            Label3.Text = "Make any necessary changes or delete current room entry below";
            Label4.Visible = true;
            CheckBox1.Visible = true;
            CheckBox2.Visible = true;
            CheckBox3.Visible = true;
            CheckBox4.Visible = true;
            CheckBox5.Visible = true;
            Button2.Visible = true;
            Button3.Visible = true;
            Button5.Visible = false;
            Label5.ForeColor = Color.Black;
            Label5.Text = "Are you sure you want to delete this room entry?";
            Label5.Visible = true;
            Button6.Visible = true;
            Button7.Visible = true;
            Button6.Focus();
        }

        protected void Button4_Click(object sender, EventArgs e)  //add option button
        {
            Label1.Visible = false;
            TextBox1.Visible = true;
            TextBox10.Visible = true;
            TextBox11.Visible = true;
            DropDownList3.Visible = true;
            FileUpload1.Visible = true;
            Label3.Visible = true;
            Label3.Text = "Enter information for new room entry below";
            Label4.Visible = true;
            CheckBox1.Visible = true;
            CheckBox2.Visible = true;
            CheckBox3.Visible = true;
            CheckBox4.Visible = true;
            CheckBox5.Visible = true;
            Button2.Visible = false;
            Button3.Visible = false;
            Button5.Visible = true;
            TextBox1.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            DropDownList3.SelectedIndex = DropDownList3.Items.IndexOf(DropDownList3.Items.FindByValue("max 1 person"));
            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
            CheckBox3.Checked = false;
            CheckBox4.Checked = false;
            CheckBox5.Checked = false;
            Label5.Visible = false;
            Button6.Visible = false;
            Button7.Visible = false;
            FileUpload1.Focus();
        }

        protected void Button5_Click(object sender, EventArgs e)  //add new room button
        {
            if (string.IsNullOrWhiteSpace(TextBox1.Text) || string.IsNullOrWhiteSpace(TextBox10.Text) || string.IsNullOrWhiteSpace(TextBox11.Text))
            {
                Label1.Text = "Please fill all required fields";
                Label1.ForeColor = Color.Red;
                Label1.Visible = true;
                TextBox1.Visible = true;
                TextBox10.Visible = true;
                TextBox11.Visible = true;
                DropDownList3.Visible = true;
                FileUpload1.Visible = true;
                Label3.Visible = true;
                Label3.Text = "Enter information for new room entry below";
                Label4.Visible = true;
                CheckBox1.Visible = true;
                CheckBox2.Visible = true;
                CheckBox3.Visible = true;
                CheckBox4.Visible = true;
                CheckBox5.Visible = true;
                Button2.Visible = false;
                Button3.Visible = false;
                Button5.Visible = true;
                Label5.Visible = false;
                Button6.Visible = false;
                Button7.Visible = false;
                FileUpload1.Focus();
            }
            else
            {
                var hotelOwnerId = Convert.ToInt16(Session["HotelOwnerId"]); //παίρνουμε το HotelOwnerId από το Session
                string hotelOwnerEmail = (string)Session["email"]; //παίρνουμε το HotelOwnerEmail από το Session
                string email = Session["email"].ToString();
                string pass = Session["pass"].ToString();


                try
                {
                    var query0 = from ho in context.HotelOwnerSet //query για εντοπισμό ήδη υπαρχόντων δωματίων με ίδιο όνομα
                                 join h in context.HotelSet on ho.Id equals h.HotelOwner.Id
                                 join r in context.RoomSet on h.Id equals r.Hotel.Id
                                 where ((ho.Email.Equals(email)) && (ho.Password.Equals(pass)) && (r.RoomNumber.Equals(TextBox1.Text)))
                                 select new { r.Id };

                    if (query0.Any()) //αν υπάρχει ήδη δωμάτιο του hotel owner με το νούμερο που εισήχθη
                    {
                        Label1.Text = "Room '" + TextBox1.Text + "' is already registered";
                        Label1.ForeColor = Color.Red;
                        Label1.Visible = true;
                        Label3.Visible = true;
                        Label3.Text = "Please choose a different room number";
                    }
                    else
                    {
                        Room newR = new Room(); //δημιουργία νέου object room
                        newR.RoomName = TextBox10.Text;
                        newR.Rate = Convert.ToDouble(TextBox11.Text, System.Globalization.CultureInfo.InvariantCulture);
                        //1 ενδιάμεσος χαρακτήρας από DropdownList, θέλουμε τον αριθμό μεταξύ των λέξεων max και persons
                        string selectedMaxPersons = DropDownList3.SelectedValue.Substring(4, 1);
                        newR.MaxPersons = Convert.ToInt16(selectedMaxPersons);
                        newR.RoomNumber = TextBox1.Text;

                        //δημιουργία directory με το number του Room ως όνομα μέσα στο directory με το mail του hotel owner για άμεση η μελλοντική αποθήκευση φωτό
                        Directory.CreateDirectory(Server.MapPath("~/HotelRoomImages//" + hotelOwnerEmail + "//" + TextBox1.Text));

                        if (FileUpload1.HasFile) //αν έχει επιλέξει ο Hotel owner φωτό για ανέβασμα
                        {
                            newR.RoomPhotoUrl = FileUpload1.FileName;
                            FileUpload1.SaveAs(Server.MapPath("~/HotelRoomImages//" + hotelOwnerEmail + "//" + TextBox1.Text + "//" + FileUpload1.FileName)); //αποθήκευση και στον φάκελο του project      
                        }

                        var query = from h in context.HotelSet
                                    join ho in context.HotelOwnerSet on h.HotelOwner.Id equals ho.Id
                                    where ho.Id == hotelOwnerId
                                    select h;  //επιλογή object ξενοδοχείου που έχει ως hotel Owner αυτόν που θέλει να προσθέσει το δωμάτιο

                        foreach (Hotel ht in query)
                        {
                            ht.Room.Add(newR); //προσθέτουμε το room στο ξενοδοχείο και μπαίνει ταυτόχρονα και στη ΒΔ
                        }

                        //προσθήκη services με βάση τα επιλεγμένα Checkboxes
                        if (CheckBox1.Checked == true) //αν το checkBox είναι checked, πρόσθεση Text του σε List
                        {
                            Service s1 = new Service();
                            s1.ServiceName = CheckBox1.Text;
                            newR.Service.Add(s1); //προσθήκη του νεοδημιουργηθέντος service σε room
                        }
                        if (CheckBox2.Checked == true)
                        {
                            Service s1 = new Service(); //νέο if, δυνατή η χρήση ξανά ενός object με όνομα Service s1
                            s1.ServiceName = CheckBox2.Text;
                            newR.Service.Add(s1); //προσθήκη του νεοδημιουργηθέντος service σε room
                        }
                        if (CheckBox3.Checked == true)
                        {
                            Service s1 = new Service();
                            s1.ServiceName = CheckBox3.Text;
                            newR.Service.Add(s1); //προσθήκη του νεοδημιουργηθέντος service σε room
                        }
                        if (CheckBox4.Checked == true)
                        {
                            Service s1 = new Service();
                            s1.ServiceName = CheckBox4.Text;
                            newR.Service.Add(s1); //προσθήκη του νεοδημιουργηθέντος service σε room
                        }
                        if (CheckBox5.Checked == true)
                        {
                            Service s1 = new Service();
                            s1.ServiceName = CheckBox5.Text;
                            newR.Service.Add(s1); //προσθήκη του νεοδημιουργηθέντος service σε room
                        }

                        context.SaveChanges(); //αποθήκευση αλλαγών στη ΒΔ

                        Label1.Text = "Room added successfully!";
                        Label1.ForeColor = Color.Green;
                        Label1.Visible = true;
                        TextBox9.Text = ""; //καθαρισμός TextBox για search νέου room
                        TextBox1.Visible = false;
                        TextBox10.Visible = false;
                        TextBox11.Visible = false;
                        DropDownList3.Visible = false;
                        FileUpload1.Visible = false;
                        Label3.Visible = false;
                        Label4.Visible = false;
                        CheckBox1.Visible = false;
                        CheckBox2.Visible = false;
                        CheckBox3.Visible = false;
                        CheckBox4.Visible = false;
                        CheckBox5.Visible = false;
                        Button2.Visible = false;
                        Button3.Visible = false;
                        Button5.Visible = false;
                        Label5.Visible = false;
                        Button6.Visible = false;
                        Button7.Visible = false;
                    }
                }
                catch (Exception exc)
                {
                    Label1.Text = "Something went wrong! Please try reloading the page";
                    Label1.ForeColor = Color.Red;
                    Label1.Visible = true;
                    TextBox1.Visible = false;
                    TextBox10.Visible = false;
                    TextBox11.Visible = false;
                    DropDownList3.Visible = false;
                    FileUpload1.Visible = false;
                    Label3.Visible = false;
                    Label4.Visible = false;
                    CheckBox1.Visible = false;
                    CheckBox2.Visible = false;
                    CheckBox3.Visible = false;
                    CheckBox4.Visible = false;
                    CheckBox5.Visible = false;
                    Button2.Visible = false;
                    Button3.Visible = false;
                    Button5.Visible = false;
                    Label5.Visible = false;
                    Button6.Visible = false;
                    Button7.Visible = false;
                }
            }
        }

        protected void Button6_Click(object sender, EventArgs e)  //'yes' button
        {
            roomId = Convert.ToInt16(Session["roomId"]); //παίρνουμε το roomId από το Session, μπήκε εκεί στην πιο πάνω method
            string hotelOwnerEmail = (string)Session["email"]; //παίρνουμε το HotelOwnerEmail από το Session

            try
            {
                //διαγραφή directory με το number του Room ως όνομα μέσα στο directory με το mail του hotel owner και των αρχείων της 
                Directory.Delete(Server.MapPath("~/HotelRoomImages//" + hotelOwnerEmail + "//" + TextBox1.Text), true);


                var rowToDelete = from r in context.RoomSet //εύρεση του δωματίου προς διαγραφή με βάση το Id
                                  where r.Id == roomId
                                  select r;

                foreach (var row in rowToDelete)
                {
                    context.RoomSet.Remove(row); //διαγραφή γραμμής σε ΒΔ 
                }

                context.SaveChanges(); //αποθήκευση αλλαγών στη ΒΔ

                Label1.Text = "Room entry deleted successfully!";
                Label1.ForeColor = Color.Green;
                Label1.Visible = true;
                TextBox9.Text = ""; //καθαρισμός TextBox για search νέου room
                TextBox1.Visible = false;
                TextBox10.Visible = false;
                TextBox11.Visible = false;
                DropDownList3.Visible = false;
                FileUpload1.Visible = false;
                Label3.Visible = false;
                Label4.Visible = false;
                CheckBox1.Visible = false;
                CheckBox2.Visible = false;
                CheckBox3.Visible = false;
                CheckBox4.Visible = false;
                CheckBox5.Visible = false;
                Button2.Visible = false;
                Button3.Visible = false;
                Button5.Visible = false;
                Label5.Visible = false;
                Button6.Visible = false;
                Button7.Visible = false;
            }
            catch (Exception exc)
            {
                Label1.Text = "Something went wrong! Please try reloading the page";
                Label1.ForeColor = Color.Red;
                Label1.Visible = true;
                TextBox1.Visible = false;
                TextBox10.Visible = false;
                TextBox11.Visible = false;
                DropDownList3.Visible = false;
                FileUpload1.Visible = false;
                Label3.Visible = false;
                Label4.Visible = false;
                CheckBox1.Visible = false;
                CheckBox2.Visible = false;
                CheckBox3.Visible = false;
                CheckBox4.Visible = false;
                CheckBox5.Visible = false;
                Button2.Visible = false;
                Button3.Visible = false;
                Button5.Visible = false;
                Label5.Visible = false;
                Button6.Visible = false;
                Button7.Visible = false;
            }
        }

        protected void Button7_Click(object sender, EventArgs e)  //'no' button
        {
            Label1.Text = "Room entry found!";
            Label1.ForeColor = Color.Green;
            Label1.Visible = true;
            TextBox1.Visible = true;
            TextBox10.Visible = true;
            TextBox11.Visible = true;
            DropDownList3.Visible = true;
            FileUpload1.Visible = true;
            Label3.Visible = true;
            Label3.Text = "Make any necessary changes or delete current room entry below";
            Label4.Visible = true;
            CheckBox1.Visible = true;
            CheckBox2.Visible = true;
            CheckBox3.Visible = true;
            CheckBox4.Visible = true;
            CheckBox5.Visible = true;
            Button2.Visible = true;
            Button3.Visible = true;
            Button5.Visible = false;
            Label5.Visible = false;
            Button6.Visible = false;
            Button7.Visible = false;
            FileUpload1.Focus();
        }
    }
}