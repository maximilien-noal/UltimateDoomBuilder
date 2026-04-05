cmake_minimum_required(VERSION 3.20)
project(BuilderNative LANGUAGES CXX C)

set(CMAKE_CXX_STANDARD 14)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_POSITION_INDEPENDENT_CODE ON)

# Collect source files
file(GLOB NATIVE_SOURCES "*.cpp" "*.h")
file(GLOB OPENGL_SOURCES "OpenGL/*.cpp" "OpenGL/*.h")
file(GLOB GL_LOAD_SOURCES "OpenGL/gl_load/*.c" "OpenGL/gl_load/*.h")
file(GLOB VPO_SOURCES "VPO/*.cpp" "VPO/*.h")

add_library(BuilderNative SHARED
    ${NATIVE_SOURCES}
    ${OPENGL_SOURCES}
    ${GL_LOAD_SOURCES}
    ${VPO_SOURCES}
)

target_include_directories(BuilderNative PRIVATE
    ${CMAKE_CURRENT_SOURCE_DIR}
    ${CMAKE_CURRENT_SOURCE_DIR}/OpenGL
)

# Platform-specific configuration
if(WIN32)
    target_link_libraries(BuilderNative PRIVATE opengl32 user32 gdi32)
    target_compile_definitions(BuilderNative PRIVATE WIN32 _WINDOWS)
    set_target_properties(BuilderNative PROPERTIES
        RUNTIME_OUTPUT_DIRECTORY "${CMAKE_SOURCE_DIR}/../Build"
        LIBRARY_OUTPUT_DIRECTORY "${CMAKE_SOURCE_DIR}/../Build"
    )
elseif(APPLE)
    find_package(OpenGL REQUIRED)
    target_link_libraries(BuilderNative PRIVATE OpenGL::GL dl)
    set_target_properties(BuilderNative PROPERTIES
        LIBRARY_OUTPUT_DIRECTORY "${CMAKE_SOURCE_DIR}/../Build"
        OUTPUT_NAME "BuilderNative"
        SUFFIX ".dylib"
    )
elseif(UNIX)
    find_package(X11 REQUIRED)
    target_link_libraries(BuilderNative PRIVATE X11 Xfixes dl)
    target_compile_definitions(BuilderNative PRIVATE UDB_LINUX=1)
    set_target_properties(BuilderNative PROPERTIES
        LIBRARY_OUTPUT_DIRECTORY "${CMAKE_SOURCE_DIR}/../Build"
        PREFIX "lib"
        OUTPUT_NAME "BuilderNative"
    )
endif()

# Optimization for Release builds
if(CMAKE_BUILD_TYPE STREQUAL "Release")
    target_compile_options(BuilderNative PRIVATE -O2)
endif()

# Debug symbols
if(CMAKE_BUILD_TYPE STREQUAL "Debug")
    target_compile_options(BuilderNative PRIVATE -g3)
endif()
