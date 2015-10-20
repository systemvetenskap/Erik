<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="FL1.index" %>


<!DOCTYPE html>

<html lang="en">
    <head>
        <meta charset="utf-8" />
        <title>Min första responsiva webbsida</title>

        <!-- länk till stilmall -->
        <link rel="stylesheet" href="stilmall.css">
        
        <!-- den responsiva stilmallen laddas in när webbläsarens fönster är mindre än 900 pixlar -->
        <link rel="stylesheet" href="responsiv.css" media="screen and (max-width: 900px)">

        <!-- anger att bredden på sidan ska följa bredden på skärmstorleken på den enhet som används -->
        <meta name="viewport" content="width=device-width, initial-scale=1">
    </head>
    <body>
        <form id="form1" runat="server">
            
        <!-- container -->
        <div class="container">
            <!-- sidhuvud -->
            <div class="header">
                <h1> Min första responsiva webbsida </h1>
            </div>
            <!-- navigeringsmeny -->
            <div class="nav">
                <ul class="clear">
                    <li><a href="#">Hem </a></li>
                    <li><a href="#">Om oss</a></li>
                    <li><a href="#">Responsiv webbdesign</a></li>
                    <li><a href="#">Cascading Style Sheet</a></li>
                    <li><a href="#">HyperText Markup</a></li>
                    <li><a href="#">Kontaktuppgfter</a></li>                                                                                                
                </ul>
            </div>
            <!-- sektioner -->
            <div class="body">
                <asp:GridView ID="GridView1" runat="server"></asp:GridView>  
            <div class="sektioner clear">
                <!-- sektion ett -->
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>

                    </HeaderTemplate>
                    <ItemTemplate>

                    </ItemTemplate>
                    <FooterTemplate>

                    </FooterTemplate>

                </asp:Repeater>
                <div class="sektion mobil">
                    <img src="bilder/1.jpg" alt="Hoppande lamm">
                    <h2>Sektion ett</h2>
                    <p>Lorem ipsum dolor sit amet, epicuri appareat mei at, in postea posidonium est, sed dolor dicant accusata an. Dicit tincidunt sea id. Usu no oblique malorum, dolore voluptatibus at eum. Te pro oblique legimus albucius.</p>
                </div>
                <!-- sektion två -->
                <div class="sektion sektion-andra mobil">
                    <img src="bilder/2.jpg" alt="Kärlekskranka fåglar">
                    <h2>Sektion två</h2>
                    <p>Lorem ipsum dolor sit amet, epicuri appareat mei at, in postea posidonium est, sed dolor dicant accusata an. Dicit tincidunt sea id. Usu no oblique malorum, dolore voluptatibus at eum. Te pro oblique legimus albucius.</p>
                </div>
                <!-- sektion tre -->
                <div class="sektion sektion-sista">
                    <img src="bilder/3.jpg" alt="Söt hund">
                    <h2>Sektion tre</h2>
                    <p>Lorem ipsum dolor sit amet, epicuri appareat mei at, in postea posidonium est, sed dolor dicant accusata an. Dicit tincidunt sea id. Usu no oblique malorum, dolore voluptatibus at eum. Te pro oblique legimus albucius.</p>
                </div>
                <div class="sektion mobil">
                    <img src="bilder/1.jpg" alt="Hoppande lamm">
                    <h2>Sektion ett</h2>
                    <p>Lorem ipsum dolor sit amet, epicuri appareat mei at, in postea posidonium est, sed dolor dicant accusata an. Dicit tincidunt sea id. Usu no oblique malorum, dolore voluptatibus at eum. Te pro oblique legimus albucius.</p>
                </div>
                <!-- sektion två -->
                <div class="sektion sektion-andra mobil">
                    <img src="bilder/2.jpg" alt="Kärlekskranka fåglar">
                    <h2>Sektion två</h2>
                    <p>Lorem ipsum dolor sit amet, epicuri appareat mei at, in postea posidonium est, sed dolor dicant accusata an. Dicit tincidunt sea id. Usu no oblique malorum, dolore voluptatibus at eum. Te pro oblique legimus albucius.</p>
                </div>
                <!-- sektion tre -->
                <div class="sektion sektion-sista">
                    <img src="bilder/3.jpg" alt="Söt hund">
                    <h2>Sektion tre</h2>
                    <p>Lorem ipsum dolor sit amet, epicuri appareat mei at, in postea posidonium est, sed dolor dicant accusata an. Dicit tincidunt sea id. Usu no oblique malorum, dolore voluptatibus at eum. Te pro oblique legimus albucius.</p>
                </div>
            </div><!--sektioner clear-->

              

          </div>
           
            <!-- sidfot -->
            <div class="footer">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </div>
        </div> </form>
    </body>
</html>

