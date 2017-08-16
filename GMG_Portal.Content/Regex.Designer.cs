﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GMG_Portal.Content {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Regex {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Regex() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GMG_Portal.Content.Regex", typeof(Regex).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^([A-Z]|[a-z]|[0-9]){6,64}$.
        /// </summary>
        public static string AccessCode {
            get {
                return ResourceManager.GetString("AccessCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^([0-9]|[a-z]|[A-Z]| |,)*$.
        /// </summary>
        public static string Address {
            get {
                return ResourceManager.GetString("Address", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^([0-9]|[a-z]|[A-Z])*$.
        /// </summary>
        public static string AlphaNumeric {
            get {
                return ResourceManager.GetString("AlphaNumeric", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to \d+(\.\d{1,2})?.
        /// </summary>
        public static string Amount {
            get {
                return ResourceManager.GetString("Amount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^[0-9]{1,10}$.
        /// </summary>
        public static string AmountInPiaster {
            get {
                return ResourceManager.GetString("AmountInPiaster", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^CO$.
        /// </summary>
        public static string BusinessContextId {
            get {
                return ResourceManager.GetString("BusinessContextId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^([01]?[0-9]{1,5})$.
        /// </summary>
        public static string CountRange {
            get {
                return ResourceManager.GetString("CountRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(0?[1-9]|[12][0-9]|3[01])[\/](0?[1-9]|1[012])[\/]\d{4}$.
        /// </summary>
        public static string Date {
            get {
                return ResourceManager.GetString("Date", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^((((((0[1-9])|([1-2][0-9])|(3[0-1]))\/(0[13578]|1[02])))|((((0[1-9])|([1-2][0-9])|(30))\/(0[469]|11)))|(((0[1-9])|(1[0-9])|(2[0-8]))\/(02)))\/([2-9][0-9]{3})|((29)\/(02)\/([2][0-9](([02468][048])|([13579][26]))))) (([0-1][0-9])|([2][0-3]))(:)(([0-5][0-9]))$.
        /// </summary>
        public static string DateTime {
            get {
                return ResourceManager.GetString("DateTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(([0]?[0-9])|(10))$.
        /// </summary>
        public static string DayCutOffBefore {
            get {
                return ResourceManager.GetString("DayCutOffBefore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(((([0]?[0-9])|(1[0-9])|(2[0-8])))|((([S][O][M])|([E][O][M]))))$.
        /// </summary>
        public static string DayOfMonth {
            get {
                return ResourceManager.GetString("DayOfMonth", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^\d*\.?\d*$.
        /// </summary>
        public static string DecimalNumber {
            get {
                return ResourceManager.GetString("DecimalNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^\s*(?=.*[0-9])\d*(?:\.\d{1,9})?\s*$.
        /// </summary>
        public static string DecimalNumberWithConditionGreaterThanOrEqualZero {
            get {
                return ResourceManager.GetString("DecimalNumberWithConditionGreaterThanOrEqualZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^\s*(?=.*[0-9])\d*(?:\.\d{0})?\s*$.
        /// </summary>
        public static string DecimalNumberWithConditionGreaterThanOrEqualZeroWhole {
            get {
                return ResourceManager.GetString("DecimalNumberWithConditionGreaterThanOrEqualZeroWhole", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^\s*(?=.*[1-9])\d*(?:\.\d{1,9})?\s*$.
        /// </summary>
        public static string DecimalNumberWithConditionGreaterThanZero {
            get {
                return ResourceManager.GetString("DecimalNumberWithConditionGreaterThanZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^([؟]|[?]|[\\]|[\/]|[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_]|[ا-يa-zA-Z0-9!@#\$%\^\&amp;*\ ,)\(+=._-]){1,1000}.
        /// </summary>
        public static string Description {
            get {
                return ResourceManager.GetString("Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^\w+([-+.&apos;]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$.
        /// </summary>
        public static string Email {
            get {
                return ResourceManager.GetString("Email", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (\d)?+(\.\d{1,2})?.
        /// </summary>
        public static string EquationAmount {
            get {
                return ResourceManager.GetString("EquationAmount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^\+?[0-9]{6,}$.
        /// </summary>
        public static string Fax {
            get {
                return ResourceManager.GetString("Fax", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$.
        /// </summary>
        public static string GUID {
            get {
                return ResourceManager.GetString("GUID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^([01]?[0-9]|2[0-3])$.
        /// </summary>
        public static string Hour {
            get {
                return ResourceManager.GetString("Hour", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}$.
        /// </summary>
        public static string IBan {
            get {
                return ResourceManager.GetString("IBan", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to \+(9[976]\d|8[987530]\d|6[987]\d|5[90]\d|42\d|3[875]\d|
        ///2[98654321]\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|
        ///4[987654310]|3[9643210]|2[70]|7|1)\d{1,14}$.
        /// </summary>
        public static string InternationalPhone {
            get {
                return ResourceManager.GetString("InternationalPhone", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$.
        /// </summary>
        public static string IPAddress {
            get {
                return ResourceManager.GetString("IPAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(\-?\d+(\.\d+)?),\s*(\-?\d+(\.\d+)?)$.
        /// </summary>
        public static string Location {
            get {
                return ResourceManager.GetString("Location", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /^[0-9]{1,5}$/.
        /// </summary>
        public static string Mailbox {
            get {
                return ResourceManager.GetString("Mailbox", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^([A-Z]|[a-z]|[0-9]|-){5,20}$.
        /// </summary>
        public static string MerchantName {
            get {
                return ResourceManager.GetString("MerchantName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^([A-Z]|[a-z]|[0-9]|-|{|}){20,64}$.
        /// </summary>
        public static string MerchTxtRef {
            get {
                return ResourceManager.GetString("MerchTxtRef", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^([؟]|[?]|[\\]|[\/]|[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_]|[ا-يa-zA-Z0-9!@#\$%\^\&amp;*\ ,)\(+=._-]){1,300}.
        /// </summary>
        public static string Name {
            get {
                return ResourceManager.GetString("Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^([؟]|[?]|[\\]|[\/]|[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_]|[ا-يa-zA-Z0-9!@#\$%\^\&amp;*\ ,)\(+=._-]){1,300}.
        /// </summary>
        public static string NameAr {
            get {
                return ResourceManager.GetString("NameAr", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^([؟]|[?]|[\\]|[\/]|[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_]|[ا-يa-zA-Z0-9!@#\$%\^\&amp;*\ ,)\(+=._-]){1,300}.
        /// </summary>
        public static string NameEn {
            get {
                return ResourceManager.GetString("NameEn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^[0-9]{1,9}.
        /// </summary>
        public static string Number {
            get {
                return ResourceManager.GetString("Number", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^[1-9]{1,9}.
        /// </summary>
        public static string NumberWithoutZero {
            get {
                return ResourceManager.GetString("NumberWithoutZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^.{1,100}.
        /// </summary>
        public static string Password {
            get {
                return ResourceManager.GetString("Password", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,15})$.
        /// </summary>
        public static string PasswordT {
            get {
                return ResourceManager.GetString("PasswordT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^[1-9][0-9]?$|^100$.
        /// </summary>
        public static string Percentage {
            get {
                return ResourceManager.GetString("Percentage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^([0-9]{5,14})*$.
        /// </summary>
        public static string Phone {
            get {
                return ResourceManager.GetString("Phone", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^([0-9]){1,3}$.
        /// </summary>
        public static string ProcessorID {
            get {
                return ResourceManager.GetString("ProcessorID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])-(((0|1)[0-9])|(2[0-3]))-(([0-5][0-9]))-(([0-5][0-9]))-([0-9]{3})$.
        /// </summary>
        public static string RequesterDateTime {
            get {
                return ResourceManager.GetString("RequesterDateTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^[0-9]{1,3}$.
        /// </summary>
        public static string SADADApplicationContextID {
            get {
                return ResourceManager.GetString("SADADApplicationContextID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^[1-5]$.
        /// </summary>
        public static string SADADChannelID {
            get {
                return ResourceManager.GetString("SADADChannelID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^SAR$.
        /// </summary>
        public static string SADADCurrency {
            get {
                return ResourceManager.GetString("SADADCurrency", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^([A-Z]|[a-z]|[1-9]|\.|_|@){5,13}$.
        /// </summary>
        public static string SADADOlpIdAlias {
            get {
                return ResourceManager.GetString("SADADOlpIdAlias", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^([A-Z]|[a-z]){1,15}$.
        /// </summary>
        public static string SADADUserPrincipleName {
            get {
                return ResourceManager.GetString("SADADUserPrincipleName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^([A-Z]|[0-9]){64}$.
        /// </summary>
        public static string SecureHash {
            get {
                return ResourceManager.GetString("SecureHash", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^[A-Z]{6}[A-Z0-9]{2}([A-Z0-9]{3})?$.
        /// </summary>
        public static string SwiftCode {
            get {
                return ResourceManager.GetString("SwiftCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(1|2)$.
        /// </summary>
        public static string TransactionAction {
            get {
                return ResourceManager.GetString("TransactionAction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(((s|S)(a|A)(r|R)))$.
        /// </summary>
        public static string TransactionCurrency {
            get {
                return ResourceManager.GetString("TransactionCurrency", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(((a|A)(r|R))|((e|E)(n|N)))$.
        /// </summary>
        public static string TransactionLanguage {
            get {
                return ResourceManager.GetString("TransactionLanguage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ((H|h)(T|t){2}(P|p)(S|s)?)\://[A-Za-z0-9\.\-]+(:[0-9]{1,5}){0,1}(/[A-Za-z0-9\?\&amp;#\=;\+!&apos;\(\)\*\-\._~%]*)*.
        /// </summary>
        public static string URL {
            get {
                return ResourceManager.GetString("URL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (?=^.{3,20}$)^[a-zA-Z][a-zA-Z0-9]*[._-]?[a-zA-Z0-9]+$.
        /// </summary>
        public static string UserName {
            get {
                return ResourceManager.GetString("UserName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$.
        /// </summary>
        public static string WebsiteUrl {
            get {
                return ResourceManager.GetString("WebsiteUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(([0]?[0-5]))$.
        /// </summary>
        public static string WeekCutOffBefore {
            get {
                return ResourceManager.GetString("WeekCutOffBefore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [a-z]+.
        /// </summary>
        public static string Words {
            get {
                return ResourceManager.GetString("Words", resourceCulture);
            }
        }
    }
}
