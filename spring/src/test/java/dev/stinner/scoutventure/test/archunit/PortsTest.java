package dev.stinner.scoutventure.test.archunit;

import com.tngtech.archunit.base.DescribedPredicate;
import com.tngtech.archunit.core.domain.JavaClass;
import com.tngtech.archunit.core.domain.JavaType;
import com.tngtech.archunit.core.importer.ClassFileImporter;
import com.tngtech.archunit.core.importer.ImportOption;
import com.tngtech.archunit.junit.AnalyzeClasses;
import com.tngtech.archunit.junit.ArchTest;
import com.tngtech.archunit.lang.ArchCondition;
import com.tngtech.archunit.lang.ArchRule;
import com.tngtech.archunit.lang.ConditionEvents;
import com.tngtech.archunit.lang.SimpleConditionEvent;

import java.util.Set;

import static com.tngtech.archunit.lang.syntax.ArchRuleDefinition.classes;


@SuppressWarnings("unused")
@AnalyzeClasses(packages = "dev.stinner.scoutventure", importOptions = ImportOption.DoNotIncludeTests.class)
public class PortsTest {

    @ArchTest
    public static final ArchRule SPI_INTERFACES_MUST_HAVE_SPECIFIC_ENDINGS = classes()
            .that().resideInAPackage("..spi..")
            .should().haveSimpleNameEndingWith("Adapter").orShould().haveSimpleNameEndingWith("Repository");
    @ArchTest
    public static final ArchRule API_INTERFACES_MUST_BE_ENDING_WITH_SERVICE = classes()
            .that().resideInAPackage("..api..")
            .should().haveSimpleNameEndingWith("Service");
    @ArchTest
    public static final ArchRule PACKAGE_API_AND_SPI_SHOULD_ONLY_CONTAIN_INTERFACES = classes()
            .that().resideInAnyPackage(
                    "..api..",
                    "..spi.."
            )
            .should().beInterfaces();

    public static DescribedPredicate<JavaClass> IMPLEMENT_SPI_INTERFACE = new DescribedPredicate<JavaClass>("implements an spi interface") {
        @Override
        public boolean test(JavaClass javaClass) {
            for (JavaType i : javaClass.getInterfaces()) {
                String name = i.getName();
                if (name.contains("ports.spi")) {
                    return true;
                }
            }
            return false;
        }
    };

    @ArchTest
    public static final ArchRule SPI_IMPLEMENTATIONS_MUST_RESIDE_IN_INFRASTRUCTURE = classes()
            .that(IMPLEMENT_SPI_INTERFACE)
            .should().resideInAPackage("..infrastructure..");

    public static DescribedPredicate<JavaClass> IMPLEMENT_API_INTERFACE = new DescribedPredicate<JavaClass>("implements an api interface") {
        @Override
        public boolean test(JavaClass javaClass) {
            for (JavaType i : javaClass.getInterfaces()) {
                String name = i.getName();
                if (name.contains("ports.api")) {
                    return true;
                }
            }
            return false;
        }
    };

    @ArchTest
    public static final ArchRule API_IMPLEMENTATIONS_MUST_RESIDE_IN_SERVICES = classes()
            .that(IMPLEMENT_API_INTERFACE)
            .should().resideInAPackage("..domain.services..");
    public static ArchCondition<JavaClass> HAVE_IMPLEMENTATION_IN_INFRASTRUCTURE = new ArchCondition<JavaClass>("has an implementation in package 'dev.stinner.scoutventure.infrastructure'") {
        @Override
        public void check(JavaClass item, ConditionEvents events) {
            var classes = new ClassFileImporter().importPackages("dev.stinner.scoutventure.infrastructure");

            boolean conditionMet = classes.stream().map(JavaClass::getInterfaces).flatMap(Set::stream).anyMatch(i -> i.getName().equals(item.getName()));

            events.add(new SimpleConditionEvent(item, conditionMet, item.getName() + " should have an implementation in package 'dev.stinner.scoutventure.infrastructure'"));
        }
    };
    @ArchTest
    public static final ArchRule SPI_INTERFACES_MUST_HAVE_IMPLEMENTATION = classes()
            .that().resideInAPackage("..spi..")
            .and().areInterfaces()
            .should(HAVE_IMPLEMENTATION_IN_INFRASTRUCTURE);
    public static ArchCondition<JavaClass> HAVE_IMPLEMENTATION_IN_SERVICES = new ArchCondition<JavaClass>("have an implementation in package 'dev.stinner.scoutventure.domain.services'") {
        @Override
        public void check(JavaClass item, ConditionEvents events) {
            var classes = new ClassFileImporter().importPackages("dev.stinner.scoutventure.domain.services");

            boolean conditionMet = classes.stream().map(JavaClass::getInterfaces).flatMap(Set::stream).anyMatch(i -> i.getName().equals(item.getName()));

            events.add(new SimpleConditionEvent(item, conditionMet, item.getName() + " should have an implementation in package 'dev.stinner.scoutventure.domain.services'"));
        }
    };
    @ArchTest
    public static final ArchRule API_INTERFACES_MUST_HAVE_IMPLEMENTATION = classes()
            .that().resideInAPackage("..api..")
            .and().areInterfaces()
            .should(HAVE_IMPLEMENTATION_IN_SERVICES);
}
