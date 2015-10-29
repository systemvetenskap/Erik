<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="FL1.index" %>


<!DOCTYPE html>

<html lang="en">
    <head>
        <meta charset="utf-8" />
        <title>Min första responsiva webbsida</title>

        <!-- länk till stilmall -->
        <link rel="stylesheet" href="stilmall.css">
        <link href="Content/bootstrap-theme.css" rel="stylesheet" />
        <link href="Content/bootstrap.css" rel="stylesheet" />
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
            <div runat="server" id="divError" class="alert alert-danger">
                 <asp:Label ID="lblError" runat="server"></asp:Label>
            </div>
            <div runat="server" id="divInfo" class="alert  alert-info">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                 <asp:Label ID="lblInfo" runat="server"></asp:Label>
            </div>
            <!-- sektioner -->
            <div class="body">
                <asp:GridView ID="GridView1" runat="server"></asp:GridView>  
            
                <!-- sektion ett -->
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                        <div class="sektioner clear">
                    </HeaderTemplate>
                    <ItemTemplate>
                     
                     <div class='<%# (Container.ItemIndex+1)%3==0 ? "sektion sektion-sista" : "sektion mobil" %>'>  
                        <img src="bilder/1.jpg" alt="Hoppande lamm">
                        <h2><%# Eval("title") %> </h2>
                        <p><%# Eval("description") %></p>
                      
                        <%# (Container.ItemIndex+1) % 3 ==0 ? "</div></div> <div class=\"sektioner clear\">":"</div>"%>
                        
                    </ItemTemplate>
                    <FooterTemplate>
                         </div><!--sektioner clear -->
                    </FooterTemplate>

                </asp:Repeater>
                <asp:Repeater ID="Repeater2" runat="server">
                    <HeaderTemplate>
                        <ul class="pagination">
                    </HeaderTemplate>
                    <ItemTemplate>
                      <li class='<%# Convert.ToBoolean(Eval("Selected")) ? "active" : "passive" %>'>
                        <asp:LinkButton ID="pager" runat="server" CommandArgument= <%# Eval("Value")%> OnClick="pager_Click" >
                            <%# Eval("Text") %>
                         
                        </asp:LinkButton>
                            </li>
                    </ItemTemplate>  
                    
                    <FooterTemplate>
                        </ul>  
                    </FooterTemplate>
                    </asp:Repeater>

                
                
           

              

     
           
            <!-- sidfot -->
            <div class="footer">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </div>
        </div> 
            </div></form>
        <script src="Scripts/jquery-1.9.1.min.js"></script>
        <script src="Scripts/bootstrap.min.js"></script>
    </body>
</html>

